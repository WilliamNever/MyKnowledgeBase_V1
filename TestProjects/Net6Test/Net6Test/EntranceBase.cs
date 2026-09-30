using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Net6Test.ConfigurationsClasses;
using Net6Test.Models;
using Net6Test.Services;
using Net6Test.StaticUtilities;

namespace Net6Test
{
    public abstract class EntranceBase
    {
        public readonly static Base0 b0 = new();

        protected IServiceCollection services;
        protected IServiceProvider provider;
        public EntranceBase()
        {
            services = new ServiceCollection();
            InitServices(services);
            provider = services.BuildServiceProvider();
            ExtensionsClass.Init(provider);

            //var cache = provider.GetService<IMemoryCache>();
            //cache.Set("A", 1);
            //cache.Set("a", 2);
        }

        protected virtual void InitServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddAutoMapper(typeof(MappingProfile1));

            #region for inject automapper without AutoMapper.Extensions.Microsoft.DependencyInjection

            // There are more pkgs in the higher versions, pay attention to the sub-pkgs in the version.
            //services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile)));
            //services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile1)));

            #endregion

            services.AddMemoryCache(x => { });
            services.AddTransient<Func<string, string, string>>(_ => (x, y) => ExtensionsClass.GetName(x, y));
            services.AddHttpClient("PostClientXy");

            services.AddScoped<TestingServiceMain>()
                .AddScoped<TestingServiceInjected>()
                ;
        }

        public abstract void MainRun();
    }
}
