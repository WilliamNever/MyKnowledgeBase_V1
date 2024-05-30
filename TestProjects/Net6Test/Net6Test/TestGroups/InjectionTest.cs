using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class InjectionTest
    {
        public static void InjectScopeTest()
        {
            InjectDomainTest().Wait();
        }

        private static async Task InjectDomainTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<sSvrs>();
            services.AddTransient<nSvrs>();
            services.AddScoped<mSvrs>();
            IServiceProvider provider = services.BuildServiceProvider();

            using var scope = provider.CreateAsyncScope();
            var ms1 = scope.ServiceProvider.GetService<mSvrs>();
            var ms2 = scope.ServiceProvider.GetService<mSvrs>();
            var ns1 = scope.ServiceProvider.GetService<nSvrs>();
            var ns2 = scope.ServiceProvider.GetService<nSvrs>();
            var ss1 = scope.ServiceProvider.GetService<sSvrs>();
            var ss2 = scope.ServiceProvider.GetService<sSvrs>();

            using var scope1 = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var ms3 = scope1.ServiceProvider.GetService<mSvrs>();
            var ms4 = scope1.ServiceProvider.GetService<mSvrs>();
            var ns3 = scope1.ServiceProvider.GetService<nSvrs>();
            var ns4 = scope1.ServiceProvider.GetService<nSvrs>();
            var ss3 = scope1.ServiceProvider.GetService<sSvrs>();
            var ss4 = scope1.ServiceProvider.GetService<sSvrs>();
        }
    }

    public class nSvrs {
        public Guid CID { get; set; } = Guid.NewGuid();
    }
    public class mSvrs:nSvrs { }
    public class sSvrs:nSvrs { }
}
