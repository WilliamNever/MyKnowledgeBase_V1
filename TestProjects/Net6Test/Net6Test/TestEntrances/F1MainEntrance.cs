using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Net6Test.Enums;
using Net6Test.Interfaces;
using Net6Test.Models;
using Net6Test.StaticUtilities;
using Newtonsoft.Json.Linq;
using StandardLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Net6Test.TestEntrances
{
    public class F1MainEntrance: EntranceBase
    {
        public override async void MainRun()
        {
            //Task.WaitAll(TestForNet6JsonSerialize());
            //Task.WaitAll(TestCutFileNameFromUrl());
            //Task.WaitAll(ReadRunningProgress());
            //Task.WaitAll(DateTimeFormatString());
            //Task.WaitAll(XMLSerializeTest());
            //Task.WaitAll(AutoMapperTest());
            //Task.WaitAll(JsonSerializerDeserializerTest());
            //Task.WaitAll(RelectClassAttribute());
            //Task.WaitAll(RelectReadingValuesTest());

            //Task.WaitAll(TasksExceptionTestAsync());

            //Task.WaitAll(TaskForTestAsync());
            //Task.WaitAll(DateTimeEqualityTest());
            //Task.WaitAll(MoqSetupSequenceTest());
            //Task.WaitAll(DateTimeFormatStringTest());
            //Task.WaitAll(ListUnitIntersectExpectTest());
            //Task.WaitAll(FlagEnumTest());

            //Task.WaitAll(NumberCalculate());
            //Task.WaitAll(HttpResponseMessage_Test());
            //Task.WaitAll(StringFormat_Test());
            //Task.WaitAll(RegexesTestService_Test());
            //Task.WaitAll(Encryption_Test());
            //Task.WaitAll(Parallel_ForEach_Test());
            Task.WaitAll(String_Test());
            //Task.WaitAll(Js_De_or_Serialize_Test());
        }

        private async Task Js_De_or_Serialize_Test()
        {
            string js = "{\"State\":\"SC\",\"CompanyName\":\"cn_xxx\",\"Address1\":\"ad_xxx\",\"City\":\"c_XXXXXX\",\"Zip\":\"xxx\",\"Phone\":\"(xxx) sxx-exxx\",\"CustomerOrderField1\":\"cof1_xxxr\",\"CustomerOrderField2\":\"cof2_xxx fww\",\"ShipToPostalAddressId\":\"xxx-xxxx-xxxx\",\"DisplayAddress\":\"xxx-xxxx-dddd-Sweeee Fvvvvvv-Avvvvv-SC-11111\",\"Attention1\":\"\",\"Address2\":\"\",\"Country\":\"\"}";
            var dics = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(js);
            var dics1 = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(js);
        }

        private async Task String_Test()
        {
            //BaseStringIndex bsi = new();// TKModel
            //var dts = bsi[nameof(BaseStringIndex.PV)];

            TKMEx tkm = new TKMEx { access_token = "atk", expires_in = 33, token_type = "ttp" };
            tkm.Item = "ds";
            tkm.item = "ds_item";
            tkm.BaseItem = "b_Item";
            tkm.TKMEx_Item = "TKMEx_Item_ex";

            TKModel bsc = tkm;
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(bsc);
            var dics = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(js);

            var js1 = System.Text.Json.JsonSerializer.Serialize(bsc, bsc.GetType());//, typeof(TKModel)
            var dics1 = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(js1)!;
            var dics2 = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(js1)!;
            var pv = dics1[nameof(TKMEx.expires_in)].GetInt32();
            var pv1 = dics1[nameof(TKMEx.TKMEx_Item)].GetString();

            var jsem = (System.Text.Json.JsonElement) dics2[nameof(TKMEx.expires_in)];
            var exIn = jsem.GetInt32();
            var jsem1 = (System.Text.Json.JsonElement) dics2[nameof(TKMEx.TKMEx_Item)];
            var exIn1 = jsem1.GetString();
            //---------------------------------------------------------------------------------------------------------------------------

            //var methods = typeof(BaseStringIndex).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            MethodInfo method = typeof(BaseStringIndex).GetMethod("GetValue", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var v1 = method.Invoke(tkm, new object[] { nameof(TKModel.access_token) });
            //var v2 = method.Invoke(tkm, new object[] { nameof(TKModel.expires_in) });
            //var v3 = method.Invoke(tkm, new object[] { nameof(TKModel.token_type) });
            //var v4 = method.Invoke(tkm, new object[] { nameof(TKModel.Item) });

            //var px = tkm.Item;
            var p1 = tkm[nameof(TKModel.access_token)];
            var p2 = tkm[nameof(TKModel.expires_in)];
            var p3 = tkm[nameof(TKModel.token_type)];
            var p4 = tkm[nameof(TKModel.Item)];
            var p5 = tkm[nameof(TKMEx.item)];
            var p6 = tkm[nameof(TKMEx.BaseItem)];
            var p7 = tkm[nameof(TKMEx.expires_in)];

            string st = "BC4452,DayOne6243,Promo9212";
            Regex regex = new(@"[a-z-A-Z]+");
            var ss = regex.Match(st).ToString();
            var sd = st.Replace(ss, "");



            string? aaa = null;// "@/";
            var rsl = aaa.ToUriEncode();
        }

        private async Task Parallel_ForEach_Test()
        {
            object lockObj = new object();
            int count = 0;
            List<int> list = new List<int>();
            for (int i = 0; i < 8 * 2; i++)
            {
                list.Add(i);
            }
            var l = list.Count;
            await Parallel.ForEachAsync(list, new ParallelOptions { MaxDegreeOfParallelism = 2 },
                async (item, cancellationToken) =>
                await Task.Run(async () =>
                {
                    lock (lockObj)
                    {
                        count++;
                        Console.WriteLine($"Enter - Count - {count}");
                    }
                    
                    Console.WriteLine(item);
                    var r = new Random();
                    var sleep = r.Next(1, 4);
                    Console.WriteLine(sleep * 1000);
                    Console.WriteLine();
                    Thread.Sleep(sleep * 1000);
                    lock (lockObj)
                    {
                        count--;
                        Console.WriteLine($"Exit - Count - {count}");
                    }
                    
                }, cancellationToken));
        }

        private async Task Encryption_Test()
        {
            string cmpOri = "ssdd";
            var isCmp = "sSDd".Equals(cmpOri, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine(isCmp);
            await Console.Out.WriteLineAsync($"{isCmp}");

            string passPhrase = Guid.NewGuid().ToString();
            var enc = new Encryption_128BlockSize();
            var encStr = enc.Encrypt("dddss", passPhrase);
            var oriStr = enc.Decrypt(encStr, passPhrase);
        }

        private async Task RegexesTestService_Test()
        {
            var svc = new RegexesTestService();
            var rsl = svc.RegexMultiSpecLetters_Test("aaaacbcbbcc");
        }

        private async Task StringFormat_Test()
        {
            var stbr = Environment.NewLine;
            string pathStr = @"http://www.aa.com/aa/bb/cc.txt";
            var ext = Path.GetExtension(pathStr);
            var fnNoEx = Path.GetFileNameWithoutExtension(pathStr);
            var fpath = Path.GetDirectoryName(pathStr);
            var st1 = string.Format("{0:D2}", "3");
        }

        private async Task HttpResponseMessage_Test()
        {
            var bytes = Encoding.UTF8.GetBytes("{\"access_token\":\"eyJraWQiOiI2YkxxQ0dTZXXXXXXXXXVo3VEQ3Y1ZTNjRxMk9NU0w0PSIsImFsZyI6IlJTMjU2In0\",\"expires_in\": 3600, \"token_type\": \"Bearer\"}");
            var resp = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new ByteArrayContent(bytes)
            };
            var isOk = resp.IsSuccessStatusCode;
            var str = await resp.Content.ReadAsStringAsync();
            var token = await resp.Content.ReadFromJsonAsync<TKModel>();
        }

        private async Task NumberCalculate()
        {
            double keepSecond = 1;
            int x = 10;
            Console.WriteLine(x - (int)x * 0.2);
            double rs = keepSecond > 0D ? keepSecond : (100 - 100 * 0.2);
        }

        private async Task FlagEnumTest()
        {
            var enop = EnOp.A | EnOp.B | EnOp.C;
        }

        private async Task ListUnitIntersectExpectTest()
        {
            string ccc = "s0.-.0ss-vvvv";
            Console.WriteLine($"{ccc.IndexOf('-')} - {ccc.LastIndexOf('-')}");
            List<string> lA = new List<string> { "1", "2", "3", "4", "5B" };
            List<string> lB = new List<string> { "4", "5b", "6", "7", "8" };
            var list = lA.Except(lB).ToList();
            var list1 = lA.ExceptBy(lB, x => x.ToLower()).ToList();
            var leftSkus = lA.Where(x => !lB.Contains(x)).ToList();
            var lc = lB.Select(x => x.ToUpper()).ToList();
        }

        private async Task DateTimeFormatStringTest()
        {
            var dtn = DateTime.Parse("2022-06-08T17:33:22.333Z");
            Console.WriteLine(dtn.ToString("yyyy-MM-dd"));
            Console.WriteLine(dtn.ToLocalTime().ToString());

            Console.WriteLine("------------------------------------->");
            List<string> list = new List<string>() {
                "d","D","f","F","g","G","M","O","R","s","t","T","u","U"
            };
            foreach (var it in list)
            {
                Console.WriteLine($"{it} - {dtn.ToString(it)}");
                Console.WriteLine(String.Format($"{{0:{it}}}", dtn));
                Console.WriteLine("------------------------------------->");
            }

            //Console.WriteLine(dtn.ToString("Y"));
        }

        private async Task MoqSetupSequenceTest()
        {
            var mock = new Mock<IDotTests>();
            
            Func<string, int> func = x => { int xxxl = x == "" ? 23 : 78; return xxxl; };
            Func<int, bool, bool> CopmareWithX = (x, bigThan) => { Console.Write($"{x} - "); return bigThan ? x > 5 : !(x > 5); };
            Expression<Func<int, bool>> lambda = x => CopmareWithX(x, true);
            Expression<Func<int, bool>> lambda1 = x => CopmareWithX(x, false);

            mock.Setup(f => f.GetIdx(It.Is(lambda))).Returns("x > 5 | xxxx");
            mock.Setup(f => f.GetIdx(It.Is(lambda1))).Returns("!(x > 5) | XXXX");


            mock.Setup(f => f.GetId(It.IsRegex("[^\\d]*$")))
                            .Returns<string>(func);

            mock.SetupSequence(f => f.GetId(It.IsRegex("^[\\d]+$")))
                .Returns(5)
                .Returns(5)
                .Returns(() => { return 3; })
                .Returns(2)
                .Returns(1)
                .Returns(0)
                .Throws(new InvalidOperationException("<<<---------->>>"));

            try {
                var IMIf = mock.Object;

                var idx = IMIf.GetId("");
                Console.WriteLine(idx);
                idx = IMIf.GetId("ss");
                Console.WriteLine(idx);
                idx = IMIf.GetId("ss64");
                Console.WriteLine(idx);

                Console.WriteLine("-------------------------");
                var idxx = IMIf.GetIdx(6);
                Console.WriteLine(idxx);
                idxx = IMIf.GetIdx(5);
                Console.WriteLine(idxx);
                Console.WriteLine("-------------------------");

                do
                {
                    var id = IMIf.GetId("64");
                    Console.WriteLine(id);
                } while (true);
            }
            catch (Exception ex) 
            { 
            }
            finally { }
        }

        private async Task DateTimeEqualityTest()
        {
            Regex regex = new Regex(@"[\d]{2}:[\d]{2}");
            var t1 = "13:22:43.222";
            var t2 = "2022-06-07 13:22:43.222";
            var t3 = "2022-06-07T13:22:43.222";
            var t4 = "2022-06-07T13:22:43.222Z";
            var t5 = "2022-06-07 13:22";
            var t6 = "2022-06-07";
            var t7 = "2022-06-07 13:";
            var t8 = "2022-06-07 13:2";

            var rsl1 = regex.IsMatch(t1);
            var rsl2 = regex.IsMatch(t2);
            var rsl3 = regex.IsMatch(t3);
            var rsl4 = regex.IsMatch(t4);
            var rsl5 = regex.IsMatch(t5);
            var rsl6 = regex.IsMatch(t6);
            var rsl7 = regex.IsMatch(t7);
            var rsl8 = regex.IsMatch(t8);

            var grp1 = regex.Matches(t2);
        }

        private async Task TaskForTestAsync()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            SemaphoreSlim sl = new SemaphoreSlim(10, 20);
            List<Task<int>> tasks = new List<Task<int>>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(TaskForTestAsync(i * 10, sl));
                //tasks.Add(Task.Run(
                //    () => { return i; }
                //    ));
            }
            var its = await Task.WhenAll(tasks);
        }

        private async Task<int> TaskForTestAsync(int i, SemaphoreSlim sl)
        {
            try
            {
                sl.Wait();
                var ss = i * 100;
                await Task.Run(() =>
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(2000);
                });
                return await Task.FromResult(i);
            }
            catch (Exception ex) {
                return await Task.FromResult(-1);
            }
            finally
            {
                sl.Release();
            }
        }

        private async Task TasksExceptionTestAsync()
        {
            int[] rsl;
            try
            {
                Task.WaitAll(AnyOthersTest(1), AnyOthersTest(2), AnyOthersTest(3));
            }
            catch (AggregateException ex)
            {
                var xx = ex.InnerExceptions;
            }
            catch (Exception ex)
            {
            }
            Console.WriteLine($"-------------------------------------------------------------------");
            try
            {
                rsl = await Task.WhenAll(AnyOthersTest(1), AnyOthersTest(2), AnyOthersTest(3));
            }
            catch (AggregateException ex)
            {
                var xx = ex.InnerExceptions;
            }
            catch (Exception ex)
            {
            }
            Console.WriteLine($"-------------------------------------------------------------------");
            int tNum = 0;
            int ss;
            do
            {
                var tt = await Task.WhenAny(AnyOthersTest(1), AnyOthersTest(2), AnyOthersTest(3));
                try
                {
                    ss = await tt;
                }
                catch (Exception ex)
                {
                }
                tNum++;
                Console.WriteLine($"-------------------------------------------------------------------");
            }
            while (tNum < 3);
        }

        private async Task<int> AnyOthersTest(int xx)
        {
            List<int> ss;
            do
            {
                ;
            } while ((ss = new List<int>()).Count != 0);

            if (xx == 2) throw new Exception($"XXXXXXXXXX - {xx}");

            Console.WriteLine($"Task - {xx} -1- {"e".Equals(null)}");

            //Console.WriteLine(7&4);
            //Console.WriteLine(7&(~4));
            //Func<string, Bxx1> xx = (ss) => { return new Bxx1(); };
            //Console.WriteLine(Math.Pow(2, 0));
            //Console.WriteLine(Math.Pow(2, 1));
            //Console.WriteLine(Math.Pow(2, 2));
            //Console.WriteLine(Math.Pow(2, 3));
            //Console.WriteLine(Math.Pow(2, 4));
            //Console.WriteLine(Math.Pow(2, 5));
            //Console.WriteLine(Math.Pow(2, 6));

            List<Base0> list = new List<Base0>() { new Base0 { Base0_Name = "base0" } };
            var l1 = JsonSerializer.Deserialize<List<Base0>>(JsonSerializer.Serialize(list));
            l1[0].Base0_Name = "b2";

            var leftRecs = list.Skip(1)/*.Skip(-100)*/.Take(20).ToList();

            if (xx == 1) throw new Exception($"XXXXXXXXXX - {xx}");
            Console.WriteLine($"Task - {xx} -2- {"e".Equals(null)}");
            return xx;
        }

        private async Task RelectReadingValuesTest()
        {
            //var ss = AppDomain.CurrentDomain.GetLifetimeService();

            var props = AppDomain.CurrentDomain.GetType().GetProperties();
            foreach (var prop in props)
            {
                Console.WriteLine($"{prop.Name} / {prop.GetValue(AppDomain.CurrentDomain)?.ToString()}");
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            var methods = AppDomain.CurrentDomain.GetType().GetMethods();
            foreach (var prop in methods)
            {
                if (prop.GetParameters().Length == 0 && (prop.ReturnParameter.ParameterType.IsValueType || prop.ReturnParameter.ParameterType == typeof(String)))
                {
                    try
                    {
                        Console.WriteLine($"{prop.Name} / {prop.Invoke(AppDomain.CurrentDomain, new object[] { })?.ToString()}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{prop.Name} / {ex.Message} / {ex.InnerException?.Message}");
                    }
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------");
            var eprops = typeof(Environment).GetProperties();
            foreach (var prop in eprops)
            {
                Console.WriteLine($"{prop.Name} / {prop.GetValue(null)?.ToString()}");
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            var emethods = AppDomain.CurrentDomain.GetType().GetMethods();
            foreach (var prop in emethods)
            {
                if (prop.GetParameters().Length == 0 && (prop.ReturnParameter.ParameterType.IsValueType || prop.ReturnParameter.ParameterType == typeof(String)))
                {
                    try
                    {
                        Console.WriteLine($"{prop.Name} / {prop.Invoke(AppDomain.CurrentDomain, new object[] { })?.ToString()}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{prop.Name} / {ex.Message} / {ex.InnerException?.Message}");
                    }
                }
            }
        }

        private async Task RelectClassAttribute()
        {
            var processId = Process.GetCurrentProcess().Id;
            var enProcessId = Environment.ProcessId;
            Console.WriteLine(processId);
            Console.WriteLine(enProcessId);

            var jsOption = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            List<string> SelectedPropNames = new List<string>();
            
            BaseOX ex_model = new BaseOX() { BaseOX_Name = "tExt_Name", BaseOX_HiFiInfor = "Ext_HiFi" };
            var str = JsonSerializer.Serialize(ex_model, jsOption);

            JsonDocument? json = JsonSerializer.Deserialize<JsonDocument>(str);
            var clType = typeof(BaseOX);

            var enumerObj = json.RootElement.EnumerateObject();
            foreach (var item in enumerObj)
            {
                var attr = clType.GetProperty(item.Name)?.GetCustomAttributes(typeof(Net6Test.AttributesModels.CustomInListAttribute), true);
                if (attr != null)
                    if (attr.Length == 0 || (attr.Length > 0 && ((attr[0] as Net6Test.AttributesModels.CustomInListAttribute)?.IsIncluded ?? false)))
                    {
                        SelectedPropNames.Add($"{item.Name} / {item.Value}");
                    }
            }
            Console.WriteLine(string.Join(Environment.NewLine, SelectedPropNames));
            //var pros = typeof(BaseOX).GetProperties();
            //foreach (var prop in pros)
            //{
            //    var attr = prop.GetCustomAttributes(typeof(Net6Test.AttributesModels.CustomAttribute), true);
            //    if (attr.Length == 0 || (attr.Length > 0 && ((attr[0] as Net6Test.AttributesModels.CustomAttribute)?.IsIncluded ?? false)))
            //    {
            //        SelectedPropNames.Add(prop.Name);
            //    }
            //}
        }

        private async Task JsonSerializerDeserializerTest()
        {
            var jsOption = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            BaseOX ex_model = new BaseOX() { BaseOX_HiFiInfor = "Ext_HiFi" };
            var str = JsonSerializer.Serialize(ex_model, jsOption);
            JsonDocument? json = JsonSerializer.Deserialize<JsonDocument>(str);
            var enumerObj = json.RootElement.EnumerateObject();
            foreach (var item in enumerObj)
            {
                var key = item.Name;
                var value = item.Value.ToString();
            }

            json = JsonSerializer.Deserialize<JsonDocument>($"[{str},{JsonSerializer.Serialize(new BaseOX() { BaseOX_HiFiInfor = "Ext_HiFi_1", Rec=1 }, jsOption)}]");
            var enumerArray = json.RootElement.EnumerateArray();
            foreach (var item in enumerArray)
            {
                var ss = item;
            }
        }

        private async Task AutoMapperTest()
        {
            var imapper = provider.GetService<IMapper>();
            ExtClassModel ex_model = new ExtClassModel() { HiFiInfor = "Ext_HiFi" };
            var Base2 = imapper.Map<Base2>(ex_model);
            var Base0 = imapper.Map<Base0>(ex_model);
            var BaseOX = imapper.Map<BaseOX>(ex_model);
            var BaseOXSep1 = imapper.Map<BaseOXSep1>(ex_model);

            //ex_model.Rec = 1000;
            ex_model.HiFiInfor = "Re_HIFI";
            ex_model.Name = "Re_ExName";
            BaseOXSep1.b1List = new List<Base1>();
            BaseOXSep1.Rec = 20;
            imapper.Map(ex_model, BaseOXSep1);
        }

        private async Task XMLSerializeTest()
        {
            string xml;
            ExtClassModel model = new ExtClassModel();
            string? ns = null;// "http://www.shutterfly.com/orderv3/shipping";

            using (var sw = new Utf8StringWriter())
            {
                using (var sww = new System.Xml.XmlTextWriter(sw) { Formatting = System.Xml.Formatting.Indented })
                {
                    XmlSerializer xSerial;
                    if (string.IsNullOrEmpty(ns))
                        xSerial = new XmlSerializer(typeof(ExtClassModel));
                    else
                        xSerial = new XmlSerializer(typeof(ExtClassModel), ns);
                    xSerial.Serialize(sww, model);
                    sww.Flush();
                    //xml = sw.ToString();
                }
            }

            using (var sww = new Utf8StringWriter())
            {
                XmlSerializer xSerial;
                if (string.IsNullOrEmpty(ns))
                    xSerial = new XmlSerializer(typeof(ExtClassModel));
                else
                    xSerial = new XmlSerializer(typeof(ExtClassModel), ns);
                xSerial.Serialize(sww, model);
                sww.Flush();
                xml = sww.ToString();
            }
            Console.WriteLine(xml);
        }

        private async Task DateTimeFormatString()
        {
            var dt = DateTime.Now;
            Console.WriteLine(dt.ToShortDateString());
            Console.WriteLine(dt.ToString("yyyy-MM-dd"));
            var dt1 = dt.ToString("yyyyMMddHHmmss_fff");
            var dt2 = dt.ToString("yyyyMMddHHmmss_fff");
            Console.WriteLine(dt.ToString("s"));
        }

        private async Task ReadRunningProgress()
        {
            var processId = Process.GetCurrentProcess().Id;
            var enProcessId = Environment.ProcessId;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            //var procs = Process.GetProcesses().Where(x =>
            //    x.ProcessName?.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) ?? false);

            var proc = Process.GetProcesses().FirstOrDefault(x =>
                x.ProcessName?.Equals("notepad", StringComparison.OrdinalIgnoreCase) ?? false);
            proc?.Kill();
            //proc?.WaitForExit();
            proc?.Close();
            Console.WriteLine("run nextly ...");

            try {
                await Task.Run(() => { throw new Exception("T_T"); });
            }
            catch (Exception ex) 
            { 
            }
        }

        private async Task TestCutFileNameFromUrl()
        {
            Uri uri = new("https://www.baidu.com/ss dd vv%20.html", true);
            var urlStr = uri.AbsolutePath;

            Console.WriteLine(CreateAssetName("https://s3.amazonaws.com/print.canva.com/PAE9C4VxNXM/oi-AX_7TReveegd/print/sheets-v3.pdf?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAJBSES44GW5SLMK7A%2F20220405%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220405T064316Z&X-Amz-Expires=566804&X-Amz-Signature=14013433509ff5683635661eac2350bdbec3a1f8f71b2ba9bab6a5427b6eb2cc&X-Amz-SignedHeaders=host&response-expires=Mon%2C%2011%20Apr%202022%2020%3A10%3A00%20GMT"));
            Console.WriteLine(CreateAssetName("https://s3.amazonaws.com/print.canva.com/PAE9C4VxNXM/oi-AX_7TReveegd/print/sheets-v3.pdf"));
        }

        private static string CreateAssetName(string assetUrl)
        {
            if (!string.IsNullOrWhiteSpace(assetUrl) && !assetUrl.Contains('?'))
            {
                var start = assetUrl.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1;
                var end = assetUrl.Length - start;

                return assetUrl.Substring(start, end);
            }
            if (!string.IsNullOrWhiteSpace(assetUrl) && assetUrl.Contains('?') && assetUrl.Contains('/'))
            {
                var start = assetUrl.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1;
                var end = assetUrl.IndexOf("?", StringComparison.OrdinalIgnoreCase) - start;

                return assetUrl.Substring(start, end);
            }

            return string.Empty;
        }

        private async Task TestForNet6JsonSerialize()
        {
            var idx = "dddss".LastIndexOf("");
            ExtClassModel bs = new ExtClassModel();
            var str = JsonSerializer.Serialize(bs,
                new JsonSerializerOptions(JsonSerializerDefaults.General)
                {
                    //WriteIndented = true,
                    //PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            Console.WriteLine(str);
            var obj = JsonSerializer.Deserialize<ExtClassModel>(str);

            var ss = "mon".GetWeekDay() ?? null;
            var ssNull = EnWeekExtensions.GetWeekDay(null);
        }
        
    }
}
