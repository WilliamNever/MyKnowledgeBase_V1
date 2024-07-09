using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast8Calculting
{
    public static class AppHost
    {
        public static IServiceProvider Providers { get; set; } = null!;
        public static void SetupHost()
        {
            var host = ConfigHost();
            SetupAppProvider(host.Services);
        }

        public static T? GetSerivce<T>()
        {
            return Providers.GetService<T>();
        }
        public static IEnumerable<T> GetSerivces<T>()
        {
            return Providers.GetServices<T>();
        }

        private static void SetupAppProvider(IServiceProvider services)
        {
            Providers = services;
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
                        config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                        config.AddJsonFile("appsettings.json", false, true);
                        //config.AddJsonFile($"appsettings.{environment}.json", true, true);
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                    })
                    .Build();
            return host;
        }
    }
}
