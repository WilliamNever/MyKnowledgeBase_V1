using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class StringChangeTests
    {
        public async static Task UrlParseQueryString_Test()
        {
            var url = "https://xxx.org.inet/xxx/vvv?sid=11&tId=22&fld=HSh[crr]&code=redcode&dType=ntype&domain=ODM";
            //var querystring = new Uri(url).ParseQueryString();
            var query = new Uri(url).Query;
            var querystring = System.Web.HttpUtility.ParseQueryString(query);
            await Console.Out.WriteLineAsync(query);
            await Console.Out.WriteLineAsync(WebUtility.UrlDecode(querystring.Get("field")));
            await Console.Out.WriteLineAsync(WebUtility.UrlDecode(querystring.Get("code")));
        }
    }
}
