using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Configuration;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
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
            var notifications = Provider.GetService<IOptions<List<NotificationModel>>>();
            return new TimerServices(1000, true, fm, notifications.Value);
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
                        config.AddJsonFile("appsettings.json", false, true);
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.Configure<List<NotificationModel>>(hostContext.Configuration.GetSection("Notifications"));
                        services.AddTransient<OutputHelperService>();
                    })
                    .Build();
            return host;
        }
    }
}
