using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Services;
using TimerNotificatoin.Core.Settings;

namespace TimerNotificatoin
{
    public static class APPHOST
    {
        public static void Register()
        {
            HOSTServices.InitHostServices(ConfigHost().Services);
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
                        services.Configure<AtuoSaveSettings>(hostContext.Configuration.GetSection(nameof(AtuoSaveSettings)));
                        services.Configure<TimerSettings>(hostContext.Configuration.GetSection(nameof(TimerSettings)));
                        services.Configure<List<ClassificationModel>>(hostContext.Configuration.GetSection("Classifications"));

                        services.AddTransient<OutputHelperService>();
                        services.AddTransient<AlertsAutoSaveService>();
                    })
                    .Build();
            return host;
        }
    }
}
