using Microsoft.Extensions.DependencyInjection;

namespace Net6Test.TestGroups
{
    public class InjectionTest
    {
        public static void InjectScopeTest()
        {
            InjectDomainTest().Wait();
        }

        public static async Task HttpClient_Test(IServiceProvider provider)
        {
            var scp = provider.CreateScope().ServiceProvider;
            var hct = scp.GetRequiredService<IHttpClientFactory>().CreateClient();
            var strContent = new StringContent(@"
{
  ""id"": 0,
}", System.Text.Encoding.UTF8, "application/json-patch+json");
            var rsp = await hct.PostAsync("https://localhost:44388/Notifications/CreateEvent", strContent);
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
