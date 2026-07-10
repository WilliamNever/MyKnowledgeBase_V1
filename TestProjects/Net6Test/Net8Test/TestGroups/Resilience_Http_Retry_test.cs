
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Net8Test.TestGroups
{
    public class Resilience_Http_Retry_test
    {
        public static async Task ReTryCall_Test(IServiceProvider provider)
        {
            var _log = provider.GetRequiredService<ILogger<Program>>();
            _log.LogError("Begin APP .......");

            var fc = provider.GetRequiredService<IHttpClientFactory>();
            var client = fc.CreateClient("MyClient");
            try
            {
                var crsp = await client.GetAsync("https://localhost:8481/Development/ZazzleForSpeed/listneworders");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }

            _log.LogError("Exit APP .......");
        }
    }
}
