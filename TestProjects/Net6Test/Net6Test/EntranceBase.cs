using Microsoft.Extensions.DependencyInjection;
using Net6Test.ConfigurationsClasses;
using Net6Test.Models;
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
        }

        protected virtual void InitServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddAutoMapper(typeof(MappingProfile1));
            services.AddMemoryCache(x => { });
            services.AddTransient<Func<string, string, string>>(_ => (x, y) => ExtensionsClass.GetName(x, y));
            services.AddHttpClient("PostClientXy");
        }

        public abstract void MainRun();
    }
}
