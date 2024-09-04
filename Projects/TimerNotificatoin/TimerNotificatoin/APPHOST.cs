using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Services;
using TimerNotificatoin.Core.Settings;

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
        public static TS GetRequiredService<TS>() where TS : notnull
        {
            return Provider.GetRequiredService<TS>();
        }
        public static IEnumerable<TS> GetServices<TS>()
        {
            return Provider.GetServices<TS>();
        }
        public static TimerServices GetTimerServices(INotificatoinMessage fm, IEnumerable<NotificationModel> notifications)
        {
            return new TimerServices(5000, true, fm, notifications ?? new List<NotificationModel>());
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
                        services.Configure<AppSettings>(hostContext.Configuration.GetSection(nameof(AppSettings)));
                        services.AddTransient<OutputHelperService>();
                    })
                    .Build();
            return host;
        }
    }
}
