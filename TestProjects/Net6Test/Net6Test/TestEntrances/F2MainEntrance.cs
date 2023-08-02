using AutoMapper;
using Cronos;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Net6Test.Enums;
using Net6Test.Interfaces;
using Net6Test.Models;
using Net6Test.Services;
using Net6Test.StaticUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Net6Test.Models.Bxx1;

namespace Net6Test.TestEntrances
{
    public class F2MainEntrance : EntranceBase
    {
        public override void MainRun()
        {
            //Task.WaitAll(SomeTests());
            //Task.WaitAll(EnumConverTests());
            //Task.WaitAll(JsonSerializeTests());
            //Task.WaitAll(ReadJsonWithoutDeserializing());
            //Task.WaitAll(CachingTest());
            //Task.WaitAll(IEnumerableListTest());
            //Task.WaitAll(ReadonlyStatObjectTest());
            //Task.WaitAll(TestStringPointTest());
            //Task.WaitAll(MD5Encoding());
            //Task.WaitAll(SkipTakeTest());
            //Task.WaitAll(ConvertionsTest());
            //Task.WaitAll(ReadAttributesStringTest());
            //Task.WaitAll(BitOperationTest<Bxx1, UserException>(new Bxx1()));
            //Task.WaitAll(ListArrayTest());
            Task.WaitAll(SplitString());
        }

        private async Task SplitString()
        {
            string sss = ",,";
            var ssArray = sss.Split(',');

            Regex regHeader = new Regex("(,)[\"]+");
            string str = "\"\"\"1\"\"\",\"\"\",aa\"\",vv\",nn,ss,,\"\"\"fff\"\"\",\"\"\"\"\"\",3\r\n".Trim();
            string str1 = "0115,Clayton,GA,\"Relocation February 17, 2020\",Security Finance";
            var matches = regHeader.Matches(str);
            var rStr = regHeader.Replace(str, m =>
            {
                if (((m.Value.Length - 1) & 1) == 1)
                    return $"XXX,XXX{m.Value.TrimStart(',')}";
                else
                    return m.Value;
            });
            var array = rStr.Split("XXX,XXX");

            var sec = array[2];
            Regex regEnd = new Regex("[\"]+(,)");
            var mEnd = regEnd.Matches(sec);
            var eStr = regEnd.Replace(sec, m =>
            {
                if (((m.Value.Length - 1) & 1) == 1)
                    return $"{m.Value.TrimEnd(',')}XXX,XXX";
                else
                    return m.Value;
            });

            Regex regWhole = new Regex("(,)[\"]+[\\s\\S]+?[\"]+(,)");
            var wMatches = regWhole.Matches(str);
        }

        private async Task ListArrayTest()
        {
            string[] strArry = { "aaa", "bbb" };
            var str = strArry.Skip(10).FirstOrDefault();

            string bsStr = "11282022";
            DateTime? isdt = DateTime.TryParseExact(bsStr, "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dt)
                    ? dt : null;
            var spl = bsStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var svw = string.Concat((bsStr??"").Reverse());
            var elm = bsStr.Split(",", StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

            string fs = @"D:\DLUPLTestFolders\CNOFeed\Sites\dfp\source\SFN\LUTABLE_07082022.csv";
            string fs1 = @"D:\DLUPLTestFolders\CNOFeed\Sites\dfp\source\SFN\HO_LUTABLE_07082022.csv";

            var fsN = Path.GetFileName(fs);
            var fs1N = Path.GetFileNameWithoutExtension(fs1);

            Dictionary<string, int> dics = new Dictionary<string, int>
            {
                { "aa1", 1 },
                { "aa2", 2 },
                { "aa3", 3 },
                { "aa4", 4 },
                { "aa5", 5 },
                { "", 6 },
                //{ null, 7}
            };

            var itm = dics.FirstOrDefault(x => x.Key == "sx");

            var list = new List<BaseOX>();
            var rlist = new List<BaseOX>();
            int ct = 10;
            for (var i = 0; i < ct; i++)
            {
                var bx = new BaseOX() { Rec = i };
                list.Add(bx);
                if ((i & 1) > 0)
                {
                    rlist.Add(bx);
                }
            }
            for (var i = 0; i < rlist.Count; i++)
            {
                int idx = list.IndexOf(rlist[i]);
                list.RemoveAt(idx);
            }
        }
        private async Task BitOperationTest<T, Ex>(T args) where T :  new()
                where Ex : Exception
        {
            var val = 7;
            var xval = val ^ 2;
            //var ex = new Ex();
            //var usex = ex.Create("OOOOOOOOO");

            var ex2 = Activator.CreateInstance(typeof(Ex), "VVVVVVVVVVVVVVVVV") as Ex;

            string ssddv = null;
            var ssd = System.Text.Json.JsonSerializer.Serialize(ssddv);
        }

        private async Task ReadAttributesStringTest()
        {
            var service = ExtensionsClass.GetImplement<Func<string, string, string>>();
            Console.WriteLine(service.Invoke("ACB", "What is that?"));

            var str = "rr aa=1 bb='here'  cc=33 bb=22 bb=\"d'\"c\" ";
            var dic = str.ToDictionaryAttributes();
        }

        private async Task ConvertionsTest()
        {
            var str = string.Join("--", new List<string> { "" });
            //var dt = DateTime.Now;
            //var sss = TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.Local);

            var utcNow = DateTime.UtcNow;
            Console.WriteLine(utcNow);
            Console.WriteLine("------------");
            var lastRun = TimeZoneInfo.ConvertTimeToUtc(utcNow, TimeZoneInfo.Utc);
            Console.WriteLine(lastRun);


            CronExpression expression = CronExpression.Parse("0 9,16 * * *");
            for (int i = 0; i < 10; i++)
            {
                var NextRunUTC = expression.GetNextOccurrence(utcNow, TimeZoneInfo.Local);
                utcNow = NextRunUTC ?? utcNow;
                Console.WriteLine(utcNow);
            }
        }

        private async Task SkipTakeTest()
        {
            var bstr = new StringBuilder("sss");
            var str = new StringBuilder(bstr.ToString());
            RedefineStr(str);

            List<int> list = new();
            for (int i = 0; i < 101; i++) list.Add(i);

            int page = 0; int size = 10;
            List<int> subList;
            do
            {
                if (page == 9)
                {
                    var pgs = page.ToString();
                }
                subList = list.Skip(size * page).Take(size).ToList();
                if (subList.Count > 0)
                    page++;
            } while (subList.Count > 0);
        }

        private void RedefineStr(StringBuilder str)
        {
            str.Append($" - New Added string");
        }

        private async Task MD5Encoding()
        {
            var str = "111111";
            var md5 = MD5.Create();
            var md5bt = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder builder = new StringBuilder();
            foreach (var item in md5bt)
            {
                builder.Append(item.ToString("X2"));
            }
            Console.WriteLine(builder.ToString());
            var str2 = BitConverter.ToString(md5bt);
            Console.WriteLine(str2.Replace("-", ""));
            var str3 = BitConverter.ToString(md5bt, 4, 8);
            Console.WriteLine(str3.Replace("-", ""));
        }

        private async Task TestStringPointTest()
        {
            int multiple = 10;
            int baseNum = 9;
            for (int i = 0; i < multiple; i++)
            {
                var muls = Math.Pow(10, i);
                Console.Write($"{i} - {muls} - {baseNum * muls} - ");
                Console.WriteLine(GetNumberString((baseNum * muls).ToString()));
            }

            Console.WriteLine(GetNumberString("1500000"));
        }

        private string GetNumberString(string weightString)
        {
            string weight;
            //if (weightString.Length == 1)
            //{
            //    weight = $"0.000{weightString}";
            //}
            //else
            {
                weight = weightString.Length > 4 ? weightString.Insert(weightString.Length - 4, ".") : weightString.PadLeft(5, '0').Insert(1, ".");
            }
            return weight;
        }

        private async Task ReadonlyStatObjectTest()
        {
            var ct = Environment.ProcessorCount;

            #region needs windows log operation permissions

            //LogWirter log = new LogWirter();
            //log.LogEvent("An Ex Error");

            #endregion

            b0.id = Guid.NewGuid();

        }

        private async Task IEnumerableListTest()
        {
            int rsl = 0;
            IEnumerable<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            var fst = list.FirstOrDefault();
            foreach (var item in list)
            {
                rsl += item;
            }
            var lst = new List<Base0> { new Base0 { Rec = 1 }, new Base0 { Rec = 2 }, new Base0 { Rec = 3 } };

            await Parallel.ForEachAsync(lst, new ParallelOptions { MaxDegreeOfParallelism = 3 }
            , async (ob, cancellationToken) => { ob.Rec = ++ob.Rec; }
            );
            ValueTask<int> vt = new ValueTask<int>(Task.Run(() => 3));
            var sds = await vt;

            var tsk = Task.Run(() => 3);
            var sdsTsk = await tsk;
        }

        private async Task CachingTest()
        {
            string key = "ssss";
            var mem = provider.GetService<IMemoryCache>();

            var itm = await mem.GetOrCreateAsync(key, e =>
            {
                return Task.FromResult(new Base0 { Base0_Name = "default B0" });
            });
            itm.Base0_Name = "Updated for B0";
            var itm2 = await mem.GetOrCreateAsync(key, e =>
            {
                return Task.FromResult(new Base0 { Base0_Name = "default B0" });
            });
        }

        private async Task ReadJsonWithoutDeserializing()
        {
            var OriJs = JObject.Parse("{\"aaxx\":3}");
            var OriJs1 = JObject.Parse("{\"aaxx\":5,cc:'Help!'}");
            OriJs.Add(new JProperty("aa", "bb"));
            OriJs.Add(new JProperty("Aa", "bb"));
            var jsStr = OriJs.ToString();
            OriJs.Merge(OriJs1);
            var jt = OriJs.SelectToken("aa");
            jt.Replace("aa");

            OriJs["aa"] = "CC";
            var ccv = OriJs["aa1"];
            OriJs["aa1"] = "xcxc";

            var sep = 0;
            #region Newtonsoft approach has the better compatibility within the two approaches to get value from a json without Deserializing.
            var jsOption = new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web)
            {
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
            };

            string jsonString = "{\"BR_NUM\":\"0685\",\"PPECategory\":\"Yes\",\"BRNUM\":\"0685\",\"HONAME\":null}";
            //jsonString = System.Text.Json.JsonSerializer.Serialize(
            //    System.Text.Json.JsonSerializer.Deserialize<object>(jsonString, jsOption));

            using (var jsn = System.Text.Json.JsonDocument.Parse(jsonString))
            {
                //JsonDocument 为非托管资源
                var val = jsn.RootElement.GetProperty("BR_NUM").GetString();
                bool isOk = jsn.RootElement.TryGetProperty("BR_NUM", out System.Text.Json.JsonElement JeVal);
                var jeVStr = JeVal.GetString();
            }

            var nJsn = Newtonsoft.Json.Linq.JToken.Parse(jsonString);
            var vala = nJsn.Value<string>("BR_NUM");

            var ifx = nJsn.SelectToken("BR_NUM");
            var ifExists = ifx.ToObject<JValue>();
            var jvl = ifExists.Value<string>();

            // a new approach to read json without Deserializing in .net6
            var jsNode = System.Text.Json.Nodes.JsonNode.Parse(jsonString);
            var jv = jsNode["BR_NUM"]?.AsValue().GetValue<string>();

            //var v1 = GetValueFromJson<int?>(jsonString, "HONAME", 1000);

            var jsString = @"{""PageInfo"":{""Success"":true,""Status"":200,""Message"":"""",""Errors"":[]},""Successful"":false,""BackofficeStatusCode"":0,""RequestSubmitted"":null,""RequestCompleted"":null,""Message"":""Unable to find order detail id: 15372""}";
            var v2 = GetValueFromJson<bool?>(jsString, "PageInfo.Success", true);
            #endregion
        }
        protected static TV GetValueFromJson<TV>(string jsonString, string Key, TV defaultValue)
        {
            var nJv = JToken.Parse(jsonString).SelectToken(Key)?.ToObject<JValue>();
            if (nJv == null) return defaultValue;
            var val = nJv.Value<TV>();
            if (val == null) return defaultValue;
            return val;
        }
        private async Task JsonSerializeTests()
        {
            string json = System.Text.Json.JsonSerializer.Serialize(
                new Base0 { 
                    Base0_Name = "XXXXX", 
                    b1List = new() { new Base1 { Base0_Name="XXXX1!" } }
                }.b1List
                );
        }

        private async Task EnumConverTests()
        {
            var b0 = new Base0 {  };
            Console.WriteLine(nameof(Base0.Rec));
            var en = Enum.TryParse("1", out EnOp enop);
        }

        private async Task SomeTests()
        {
            var imapper = provider.GetService<IMapper>();
            ExtClassModel ecm = new ExtClassModel() { HiFiInfor = "ex HIFI", Acgx = "33xx" };
            var b2 = imapper.Map<Base2>(ecm);
            Console.WriteLine(DateTime.Now.DayOfYear);
        }
    }
}
