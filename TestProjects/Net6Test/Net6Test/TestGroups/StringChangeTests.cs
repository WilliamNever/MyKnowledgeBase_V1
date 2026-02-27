using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic.FileIO;
using Net6Test.StaticUtilities;
using StandardLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

        public async static Task StringEndsSub_Test()
        {
            string ss = "VD_ASEFF23FE.FasdfRe__28";
            var exp = new Regex(@"[\d]+$", RegexOptions.RightToLeft);
            var mc = exp.Match(ss);
            var mcs = exp.Matches(ss);
        }

        public async static Task SomeStringConvertTest()
        {
            string mmx = "True";
            var rslt = bool.TryParse(mmx, out var rsl);
            //var brsl = bool.Parse(mmx);
            var fileName = @"D:\WorkSpaces\DevAzure\ssd\ffv.wve\sde\bsm.cs";
            var fn = Path.GetFileName(fileName);
            //fn = null;
            var isEqual = fn.ToEquals("BSm.cs");

            var uri = new Uri("ttcdb://rssl/" + "53d2afdf-b9ad-461d-99d9-90ca3f09c1a8");
            var p1 = Guid.Parse(uri.PathAndQuery.TrimStart('/'));
            var uri1 = new Uri(@"\\fallec\Software\Free tools\Chrome\");
            var uri2 = new Uri(@"http://www.baidu.com/ori/xxx.xml");
            var uri3 = new Uri(@"https://www.baidu.com/xx?xxv=1&xvve=3x");
        }

        public static void PathCombine()
        {
            var sb = @"d:\tmp";
            var sp = "xxx/11_jioi.zip";
            
            var fp = Path.Combine(sb, sp);
            var fp2 = Path.Combine("/", sp);
            var fp1 = Path.GetFullPath(sp);
        }

        public static void StringTrimTest()
        {
            var str = " ;  ;; aaa;;  ; ;";
            var des = str.Trim("; ".ToCharArray());
        }

        public static async Task Regex_Replace_Test()
        {
            var reg = new Regex($"^(sp-)", RegexOptions.IgnoreCase);
            var str = reg.Replace("SP-sp-aaa=bbb", "", 1, 0);
        }

        public static async Task StringJoin_Test()
        {
            var list = new List<string>() { "aa", null, "bb" };
            var str = string.Join(",", list);
        }

        public static async Task FileNames_Test()
        {
            var fp = @"D:\WorkSpaces\DevAzure\EMG.API\EMG.ResubmitMessagesMassSave.Infrstructure.Services\Utilities\CronUtilities.cs";
            var dfn = Path.GetDirectoryName(fp);
            var fn = Path.GetFileName(fp);
            var ffp = Path.GetFullPath(fp);
        }
    }
}
