using F8C.Core.Interfaces;
using F8C.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                        config.AddJsonFile("appsettings.json", true, true);
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddTransient<ICreate8Method, Simple_HH_MM_Service>();
                        services.AddTransient<ICreate8Method, Date_Time_Service>();
                        services.AddTransient<ICreate8Method, Obj_direct_Service>();
                    })
                    .Build();
            return host;
        }
    }
}
