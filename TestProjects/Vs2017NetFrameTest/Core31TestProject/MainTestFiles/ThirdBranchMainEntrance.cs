using Core31TestProject.Enums;
using Core31TestProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core31TestProject.MainTestFiles
{
    public class ThirdBranchMainEntrance : TestBaseForMainEntrance
    {
        public override void MainRun()
        {
            //Task.WaitAll(FirstTest());
            //Task.WaitAll(JsonSerialTest());
            //Task.WaitAll(TaskDiscardTest());
            //Task.WaitAll(DynamicTypeTest());
            //Task.WaitAll(ArrayListTest());
            //Task.WaitAll(TypeOfTest());
            //Task.WaitAll(ReflectTest());
            //Task.WaitAll(RegexTest());
            //Task.WaitAll(ConvertTest());
            //Task.WaitAll(UrlEnDeCode());
            //Task.WaitAll(DictionaryTest());
            //Task.WaitAll(ListArrayTest());
            //Task.WaitAll(StringFormatTest());
            Task.WaitAll(JsonTests());
            //Task.WaitAll(ToStringValue());
            //Task.WaitAll(ReflectTestAsync());
            //Task.WaitAll(PostFileAsFormTestAsync());
        }

        private async Task PostFileAsFormTestAsync()
        {
            string test_json = "{\"name\":\"tom\",\"nickname\":\"tony\",\"sex\":\"male\",\"age\":20,\"email\":\"123@123.com\"}";
            dynamic aa = Newtonsoft.Json.JsonConvert.DeserializeObject(test_json);
            Console.WriteLine(aa.nameX == null);

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
            sw.WriteLine(@"Haha \O.O/");
            sw.Flush();
            ms.Flush();
            var bytes = ms.ToArray();
            sw.Close();
            ms.Close();
            HttpClient client = new HttpClient();
            MultipartFormDataContent mfd = new MultipartFormDataContent();
            mfd.Add(new StringContent("name"), "name");
            mfd.Add(new ByteArrayContent(bytes), "fCollection", "AA.TXT");
            await client.PostAsync("http://localhost:56024/InputTEST/AjaxUpdate", mfd);
        }

        public async Task ReflectTestAsync()
        {
            string test = "abcdefghijklmnopqrstuvwxyz";

            // Get a PropertyInfo object representing the Chars property.
            PropertyInfo pinfo = typeof(string).GetProperty("Chars");

            // Show the complete string.
            Console.Write("The entire string: \n");
            for (int x = 0; x < test.Length; x++)
            {
                Console.Write(pinfo.GetValue(test, new Object[] { x }));
                if (x + 1 < test.Length)
                    Console.Write(" - ");
            }
            Console.WriteLine();

            var indexer = new Object[] { 9 };
            Console.WriteLine(pinfo.GetValue(test, indexer));
        }

        private async Task ToStringValue()
        {
            ExValue idi = new ExValue
            {
                Id = "aa",
                Products = 55
            };
            var key = idi["id"];

            Console.WriteLine(true.ToString());
        }

        private async Task JsonTests()
        {
            //string test_json = "{\"name\":\"tom\",\"nickname\":\"tony\",\"sex\":\"male\",\"age\":20,\"email\":\"123@123.com\"}";
            string test_json = "{11:12,12:33}";
            var ojson = Newtonsoft.Json.JsonConvert.DeserializeObject(test_json) as Newtonsoft.Json.Linq.JObject;

            var array = ojson.Properties().ToArray();
            foreach (var p in array)
            {
                Console.WriteLine($"key={p.Name} / value={p.Value}");
            }
        }

        private async Task StringFormatTest()
        {
            var strUnencode = System.Web.HttpUtility.UrlEncode(@"0.*/\% ");
            Console.WriteLine(strUnencode);
            DateTime? dt = DateTime.Now;
            Console.WriteLine(dt.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private async Task ListArrayTest()
        {
            string aaa = "aaa";
            object sss = new string[] { "aaa","bbb" };
            List<string> strs = new List<string> { "aaa", "bbb" };
            Console.WriteLine(aaa is IList);
            Console.WriteLine(sss is IList);
            Console.WriteLine(strs is IList);

            object strList = new List<string> { "aaa", "bbb" };
            string val = "";
            if (sss is IList)
            {
                foreach (var itm in sss as IList)
                {
                    val += $"{itm} - ";
                }
            }

            Func<IEnumerable<string>, IEnumerable<string>> mailFilter = x => {
                IEnumerable<string> reVal = null;
                if (x != null)
                {
                    List<string> removeAddresses = new List<string> {
                        "oneplace@taylorcommunications.com",
                        "vendororders@workflowone.com"
                    };
                    reVal = x.Except(removeAddresses);
                }
                return reVal;
            };
            var list = mailFilter(
                new List<string> {
                        "oneplace@taylorcommunications.com",
                        "vendororders@workflowone.com",
                        "hcc@taylor.com"
                    }
                ).ToList();
        }

        private async Task DictionaryTest()
        {
            Dictionary<string, string> dds = new Dictionary<string, string>();
            dds.Add("aa", "aa-aa");
            //dds.Add("aa", "aa-11");

            var cc = dds.TryGetValue("aa", out string mm);
            mm = "vvvv";

            if (dds.ContainsKey("aa"))
            {
                dds.Add("bb", "bb-bb");
            }
            else
            {
                dds.Add("aa", "aa-aa");
            }
            dds["aa"] = "cc-cc";
        }

        private async Task UrlEnDeCode()
        {
            var str = "(1U) EXAM ONLY GRID\\/*+";
            Console.WriteLine(System.Web.HttpUtility.UrlPathEncode(str));
            Console.WriteLine(System.Web.HttpUtility.UrlEncode(str));
            Console.WriteLine(System.Web.HttpUtility.HtmlEncode(str));
        }

        private async Task ConvertTest()
        {
            string val = "Promo!";
            var resl = ConvertToEnum<EnOrderType>(val);
            if (resl != EnOrderType.Promo)
                Console.WriteLine(val);
            else
                Console.WriteLine(resl.ToString());
        }
        public static T? ConvertToEnum<T>(string str) where T : struct
        {
            T? resl = null;
            if (Enum.TryParse(str, true, out T re))
            {
                resl = re;
            }
            return resl;
        }

        private async Task RegexTest()
        {
            var strObj = "[\"aa\",1,false]";
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(strObj);

            var dt = DateTime.Now;
            object odt = dt;
            var dsf = odt as DateTime?;
            Console.WriteLine(dsf.HasValue ? dsf.Value.ToShortTimeString() : "NULL");
            Console.WriteLine(dt.ToString());
            Console.WriteLine(dt.ToString("MM/dd/yyyy"));
            //Console.WriteLine(sdt.ToString("MM/dd/yyyy"));


            string str = "PaymentMethod";
            //camelString.replace(/([A-Z]+)/g, " $1").replace(/([A-Z][a-z])/g, " $1");
            Regex reg1 = new Regex("([A-Z]+)");
            Regex reg2 = new Regex("([A-Z][a-z])");
            Console.WriteLine(reg2.Replace(reg1.Replace(str, " $1"), " $1"));
        }

        private async Task ReflectTest()
        {
            var method = typeof(int).GetMethod("TryParse", new Type[] { typeof(string), typeof(int).MakeByRefType() });
            int tt = 0;
            object[] parmsObj = new object[] { "123", tt };
            var isok = method.Invoke(null, parmsObj);

            NameContainer nc = new NameContainer { FirstName = "fn", MiddleNames = "mn", LastName = "ln" };
            var type = nc.ID.GetType();
            Console.WriteLine(type == typeof(string));

            var sit = Convert.ToInt32(nc);

            var dt = DateTime.Now;
            Console.WriteLine(dt.ToString("MM/dd/yyyy"));
            Console.WriteLine(dt.ToShortDateString());
            var fstr = true.ToString();
            var fsbool = bool.Parse(fstr);
            var ss = typeof(string);
            Console.WriteLine();
            Console.WriteLine(typeof(int).IsValueType);
            Console.WriteLine(typeof(KeyValuePair<string, string>).IsValueType);
            Console.WriteLine(typeof(string).IsValueType);
            Console.WriteLine(typeof(Base0).IsValueType);
        }

        public static T GetPropertyValue<T>(object obj, string FName, params object[] index)
        {
            var prop = obj.GetType().GetProperty(FName, PropertyFlags);
            if (prop == null)
            {
                return default;
            }
            else
            {
                var val = prop.GetValue(obj, index);
                var method = typeof(T).GetMethod("TryParse", PropertyFlags);
                //T rsl;
                //var ok = method.Invoke(val.ToString(),  out rsl);
                return default;
            }
        }
        private readonly static BindingFlags PropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        private async Task TypeOfTest()
        {
            NameContainer nc = new NameContainer { FirstName = "fn", MiddleNames = "mn", LastName = "ln" };
            var nd = nc;
            var pl =
            //nc;
            Newtonsoft.Json.JsonConvert.DeserializeObject<NameContainer>(
                Newtonsoft.Json.JsonConvert.SerializeObject(nc)
                );
            nc.FirstName = "Updated";
            //Console.WriteLine(pl["FirstName"]);
            //Console.WriteLine(pl.FirstName2==null);
            Console.WriteLine(nc.FirstName);

            Base0 b0 = new Base0
            {
                Rec = 1,
                b1List = new List<Base1> {
                    new Base1{ Acgx="aa"  },
                    new Base1{ Acgx="bb"  },
                    new Base1{ Acgx="AA"  },
                    new Base1{ Acgx=""  },
                    new Base1{ Acgx=null  },
                    new Base1{ Acgx="ee"  },
                }
            };

            var ss = GetProPName(b0, x => x.b1List[0].Rec);

            Expression<Func<NameContainer, string>> nExpress = x => x.FirstName;



            Expression<Func<NameContainer, string>> expression = x => x.FirstName;
            var ssdm = expression.Compile()(nc);
        }

        private string GetProPName<T, S>(T objValue, Expression<Func<T, S>> expression)
        {
            var stp = typeof(S);
            object o = expression;
            string path = expression.Body.NodeType == ExpressionType.MemberAccess ? "Body.Member.Name" : "Body.Operand.Member.Name";
            path.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(point =>
            {
                o = o.GetType().GetProperty(point,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).GetValue(o);
            });

            var vls = expression.Compile().Invoke(objValue);


            return o?.ToString();
        }

        private async Task ArrayListTest()
        {
            string str = null;
            var url = System.Web.HttpUtility.UrlEncode(str);
            var boolString = false.ToString() + "/" + true.ToString();
            var surl = $"{url}{false}";
            bool? ask = null;
            Console.WriteLine(ask == null);
            var list = new List<string> { "aa", "bb", "cc", "dd", "ee" };//
            Console.WriteLine(list[^1]);

            Console.WriteLine("------------------");

            Base0 b0 = new Base0
            {
                Rec = 1,
                b1List = new List<Base1> {
                    new Base1{ Acgx="aa"  },
                    new Base1{ Acgx="bb"  },
                    new Base1{ Acgx="AA"  },
                    new Base1{ Acgx=""  },
                    new Base1{ Acgx=null  },
                    new Base1{ Acgx="ee"  },
                }
            };
            dynamic aaa = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(
                Newtonsoft.Json.JsonConvert.SerializeObject(b0)
                );

            Console.WriteLine(aaa.b1List!=null);
            Console.WriteLine(aaa.b1List is IList);

            foreach (var itm in aaa.b1List)
                Console.WriteLine($"{itm.Acg} - {itm.Acgx?.ToString().Trim()}");

            var b1List = b0.b1List.OrderBy(x => x.Acgx).Select(x=>x.Acgx).ToList();

            int[] aaInt = new int[] { 11, 12, 13, 14, 15, 16 };
            Array aasArray = aaInt;
        }

        private async Task DynamicTypeTest()
        {
            string jsString = "{Id:'idtttt'}";
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsString);
            DimensionItem dmim=(DimensionItem)obj;
            Console.WriteLine(obj.Id);
            Console.WriteLine(obj is DimensionItem);
            Console.WriteLine(obj.Ids==null);
            Console.WriteLine(obj["Id"]);

            //dynamic aa = new DimensionItem { Id = "id" };
            //Console.WriteLine(aa is DimensionItem);
            //Console.WriteLine(aa is string);
        }

        private async Task TaskDiscardTest()
        {
            int maxLoop = 10;
            _ = Task.Run(()=> {
                for (int i = 0; i < maxLoop; i++)
                {
                    Console.WriteLine($"t1 - {i}");
                    Thread.Sleep(1000);
                }
            });
            var t2 = Task.Run(()=> {
                for (int i = 0; i < maxLoop; i++)
                {
                    Console.WriteLine($"t2 - {i}");
                    Thread.Sleep(500);
                }
            });

            //await t1;
            await t2;

        }

        private async Task JsonSerialTest()
        {
            Base0 b0 = new Base2() { Rec=1, Acg=2, Acgx="b1_Acgx", Acx=3 };
            b0.Rec = 0;

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(b0));

            var ssdf = "ssssfff"[0..3];

            double numfir = 5;
            int divided = 4;
            var nump = Math.Ceiling(numfir / divided);

            string aa = "aa", bb = "bb";
            var url = $"{aa + bb}?dl={{0}}&q={{1}}&rpp={{2}}&sort={{3}}&page={{4}}";
            Console.WriteLine(url);

            var oriUrl = "dl=category,supplier,price,color,material,size&page=1&q=0000+price:39:44%7C%7C1+size:4294308696:4293936722:4292570631%7C%7C2+preferred:1,2,3,4,5&rpp=20&sort=PVRN";
            var deUrl = System.Web.HttpUtility.UrlDecode(oriUrl);
            Console.WriteLine(deUrl);

            var enUrl = System.Web.HttpUtility.UrlEncode(deUrl);
            Console.WriteLine(enUrl);

            Console.WriteLine(oriUrl.Equals(enUrl));

            System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
            //Dictionary<string, string> hashtable = new Dictionary<string, string>();
            hashtable.Add("aa", @"aa""");
            hashtable.Add("bb", "bb");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(hashtable));



            //var dic = new Dictionary<string, List<KeyValuePair<string, string>>>();
            //dic.Add("aa", new List<KeyValuePair<string, string>> { 
            //    new KeyValuePair<string, string>("a1", "a1v"),
            //    new KeyValuePair<string, string>("a2","a2v"),
            //    new KeyValuePair<string, string>("a3","a3v")
            //});

            //dic.Add("bb", new List<KeyValuePair<string, string>> {
            //    new KeyValuePair<string, string>("b1", "b1v"),
            //    new KeyValuePair<string, string>("b2","b2v"),
            //    new KeyValuePair<string, string>("b3","ab3v")
            //});

            var dic = new System.Collections.Specialized.StringDictionary();
            dic.Add("aaa", "bbb");
            dic.Add("ccc", "ddd");
            var val = dic["aaa"];
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
            Console.WriteLine(str);

            string classString = @"
                {""Sizes"": [
                      {
                        ""Id"": ""4293979073"",
                        ""Name"": ""Other Sizes"",
                        ""ContextPath"": ""4294308696:4293979073||1"",
                        ""Products"": 33
                      },
                      {
                        ""Id"": ""4293936722"",
                        ""Name"": "">= 60\"""",
                        ""ContextPath"": ""4294308696:4293936722||1"",
                        ""Products"": 19
                      },
                      {
                        ""Id"": ""4293770119"",
                        ""Name"": ""Standard & Numbered - Other"",
                        ""ContextPath"": ""4294308696:4293770119||1"",
                        ""Products"": 19
                      },
                      {
                        ""Id"": ""4293999022"",
                        ""Name"": ""12\"" - 18\"""",
                        ""ContextPath"": ""4294308696:4293999022||1"",
                        ""Products"": 18
                      }],
                ""Colors"": [
                      {
                        ""Id"": ""4293979073"",
                        ""Name"": ""Other Sizes"",
                        ""ContextPath"": ""4294308696:4293979073||1"",
                        ""Products"": 33
                      },
                      {
                        ""Id"": ""4293936722"",
                        ""Name"": "">= 60\"""",
                        ""ContextPath"": ""4294308696:4293936722||1"",
                        ""Products"": 19
                      },
                      {
                        ""Id"": ""4293770119"",
                        ""Name"": ""Standard & Numbered - Other"",
                        ""ContextPath"": ""4294308696:4293770119||1"",
                        ""Products"": 19
                      },
                      {
                        ""Id"": ""4293999022"",
                        ""Name"": ""12\"" - 18\"""",
                        ""ContextPath"": ""4294308696:4293999022||1"",
                        ""Products"": 18
                      }]
                }";

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<DimensionItem>>>(classString);
        }

        private async Task FirstTest()
        {
            NameContainer nc = new NameContainer { FirstName="fn", MiddleNames="mn", LastName="ln" };
            Console.WriteLine($"|{nc}|");
            Console.WriteLine(nc.GetFullName());

            string ssst = "aaa";
            ssst ??= new string("Hello");
            Console.WriteLine(DateTime.UtcNow.ToString("yyyyMMdd").Substring(1));
            string str = "_ssss";
            var indexOf = str.IndexOf('_');
            var subStr = str.Substring(0, indexOf);
            var flist = Directory.GetFiles("D:\\TmpDocs");
            var fn = Path.GetFileName(flist[0]);
        }
    }
}
