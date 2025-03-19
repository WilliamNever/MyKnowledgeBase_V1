using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic.FileIO;
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
            //FileSystem.DeleteFile(@"D:\DriverSpaceMonitor\CDriver_files_2023-06-19_20215.txt", UIOption.AllDialogs, RecycleOption.SendToRecycleBin);

            var url = "https://xxx.org.inet/xxx/vvv?sid=11&tId=22&fld=HSh[crr]&code=redcode&dType=ntype&domain=ODM";
            //var querystring = new Uri(url).ParseQueryString();
            var query = new Uri(url).Query;
            var querystring = System.Web.HttpUtility.ParseQueryString(query);
            await Console.Out.WriteLineAsync(query);
            await Console.Out.WriteLineAsync(WebUtility.UrlDecode(querystring.Get("DoMain")));
            await Console.Out.WriteLineAsync(WebUtility.UrlDecode(querystring.Get("dtype")));
            await Console.Out.WriteLineAsync(WebUtility.UrlDecode(querystring.Get("field")));
            await Console.Out.WriteLineAsync(WebUtility.UrlDecode(querystring.Get("code")));

            var tt = "string cut";// await Console.In.ReadLineAsync();
            Console.WriteLine(StringValues.Empty.ToString() == string.Empty);
            string str = "0123456789";
            await Console.Out.WriteLineAsync($"{tt} - {str[4..5]}");

            Console.WriteLine("Date time - ");
            await Console.Out.WriteLineAsync(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            await Console.Out.WriteLineAsync(DateTime.Now.ToString("zzz"));
        }
    }
}
