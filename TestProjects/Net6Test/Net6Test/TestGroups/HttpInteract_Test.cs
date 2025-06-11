using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class HttpInteract_Test
    {
        public static async Task HttpClientFactory_Test(IServiceProvider provider)
        {
            var factory = provider.GetService<IHttpClientFactory>();
            var client = factory.CreateClient();
            client.BaseAddress = new Uri("https://www.baidu.com");
            var txt = await (await client.GetAsync("https://www.baidu.com")).Content.ReadAsStringAsync();
        }
    }
}
