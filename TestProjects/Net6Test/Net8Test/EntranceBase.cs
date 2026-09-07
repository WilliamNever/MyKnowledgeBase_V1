using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Polly;

namespace Net8Test
{
    public abstract class EntranceBase
    {
        public IServiceCollection services = new ServiceCollection();
        public IServiceProvider provider;
        protected EntranceBase()
        {
            InitServices(services);
            provider = services.BuildServiceProvider();
        }
        public abstract Task Entrance();

        public virtual void InitServices(IServiceCollection services)
        {
            services.AddLogging(lb => {
                lb.ClearProviders();
                lb.AddNLog("nlog.config");
            });
            services.AddHttpClient();

            services.AddHttpClient("MyClient", c => c.Timeout = TimeSpan.FromSeconds(150))
            .AddResilienceHandler("custom-retry", (pipeline, rc) =>
            {
                var log = rc.ServiceProvider.GetRequiredService<ILogger<Program>>();
                pipeline.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 5,
                    //Delay = TimeSpan.FromSeconds(5),
                    DelayGenerator = static args =>
                    {
                        var delay = TimeSpan.FromSeconds((args.AttemptNumber * 5) + 10);
                        return new ValueTask<TimeSpan?>(delay);
                    },
                    //BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                        .Handle<Exception>()
                        .HandleResult(response => !response.IsSuccessStatusCode),
                    OnRetry = arg =>
                    {
                        return new ValueTask(Task.Run(async () =>
                        {
                            log.LogError($"In on Try - {arg.AttemptNumber}" +
                                $" - {arg.Duration.TotalSeconds}" +
                                $" - {arg.RetryDelay.TotalSeconds}");
                        }));
                    },
                });

                pipeline.AddTimeout(new HttpTimeoutStrategyOptions
                {
                    Timeout = TimeSpan.FromSeconds(10),
                    OnTimeout = arg =>
                    {
                        log.LogError(
                            $"Request timeout after {arg.Timeout.TotalSeconds} seconds. " +
                            $" IsCancellationRequested - {arg.Context.CancellationToken.IsCancellationRequested}"
                            + $" ContinueOnCapturedContext - {arg.Context.ContinueOnCapturedContext}"
                           );

                        return ValueTask.CompletedTask;
                    }
                });
            });
        }

    }
}
