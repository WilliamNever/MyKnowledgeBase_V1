using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StandardLibrary.ScheduledTaskServiceTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ResubmitMessagesMassSave
{
    /// <summary>
    /// Here is the Main IHostedService to define how to invoke the tasks in template.
    /// </summary>
    public abstract class WorkerBase : BackgroundService
    {
        /// <summary>
        /// the interval to filter out available task/s
        /// </summary>
        public const int _interval = 1 * 60 * 1000;

        private readonly ILogger<WorkerBase> _logger;
        private IServicesFactory _serviceFactory;
        private List<IScheduledTask> _scheduledTasks;
        public WorkerBase(ILogger<WorkerBase> logger, IServicesFactory ServiceFactory)
        {
            _logger = logger;
            _serviceFactory = ServiceFactory;
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(StartAsync)} - {DateTimeOffset.Now}");
            using (var scope = _serviceFactory.RefreshProviderScope())
            {
                var provider = scope.ServiceProvider;
                _scheduledTasks = _serviceFactory.GetRequiredServices<IScheduledTask>(provider).Where(x => x.IsEnabled).ToList();
            }
            await base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            if (_scheduledTasks != null)
            {
                foreach (var task in _scheduledTasks)
                {
                    _ = task.SetupAsync(stoppingToken);
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    foreach (var task in _scheduledTasks)
                    {
                        _ = task.ExecuteAsync(stoppingToken);
                    }

                    try
                    {
                        await Task.Delay(_interval, stoppingToken);
                    }
                    catch (OperationCanceledException ocex)
                    {
                        _logger.LogError(ocex, $"Exited by manully stop.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Unknown Error raised.");
                    }
                }
            }
            await Task.CompletedTask;
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start - {nameof(StopAsync)} - {DateTimeOffset.Now}");
            await base.StopAsync(cancellationToken);
            _scheduledTasks?.Clear();
            _logger.LogInformation($"Exit - {nameof(StopAsync)} - {DateTimeOffset.Now}");
        }
    }
}
