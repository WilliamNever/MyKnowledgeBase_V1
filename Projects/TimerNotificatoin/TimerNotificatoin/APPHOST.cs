using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Services;

namespace TimerNotificatoin
{
    public static class APPHOST
    {
        private static IServiceProvider Provider;
        static APPHOST()
        {
            Provider = ConfigHost().Services;
        }

        public static TS? GetService<TS>()
        {
            return Provider.GetService<TS>();
        }
        public static IEnumerable<TS> GetServices<TS>()
        {
            return Provider.GetServices<TS>();
        }
        public static TimerServices GetTimerServices(INotificatoinMessage fm)
        {
            return new TimerServices(1000, true, fm);
        }

        private static IHost ConfigHost()
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var host = new HostBuilder()
                    .ConfigureHostConfiguration(hostConfig =>
                    {
                        hostConfig.AddEnvironmentVariables();
                    })
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddEnvironmentVariables(environment);
                        config.SetBasePath(ConstsParams.RootDirectory);
                        config.AddJsonFile("appsettings.json", true, true);
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddTransient<OutputHelperService>();
                    })
                    .Build();
            return host;
        }
    }
}
