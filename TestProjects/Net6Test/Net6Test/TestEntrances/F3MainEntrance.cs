using Microsoft.Extensions.Caching.Memory;
using Net6Test.Models;
using Net6Test.TestGroups;
using Newtonsoft.Json;
using StandardLibrary.Helpers;
using System.Text.Json;

namespace Net6Test.TestEntrances
{
    public class F3MainEntrance : EntranceBase
    {
        public override void MainRun()
        {
            //InitialTest().Wait();
            //DictionaryTest().Wait();
            //JsonSerializerDeserializeTest().Wait();
            //XmlDeserializeTest().Wait();
            //FuncTest().Wait();
            //new Assembly_Reflect_Tests().ReflectFindServicesTest_Part1().Wait();
            //new Assembly_Reflect_Tests().ReadRunningProgress().Wait();
            //new Assembly_Reflect_Tests().ReflectFindServicesTest().Wait();

            //SynSugarTest().Wait();
            //QuartzCronExpressionTestAsync().Wait();
            //ValueTupleTest().Wait();

            //ThreadTasksTest.ConcurrentBag_T_Test().Wait();
            //ConversionTest.TypeTest().Wait();
            //CollectionListTest.DictionaryExtensionsModel_Test().Wait();

            //InjectionTest.InjectScopeTest();
            //RecordTest.Test1Async().Wait();
            //RecordTest.Test2Async().Wait();
            //Type_Reflect_Tests.RunTest();
            //DateTime_Tests.Test().Wait();
            //MemoryCache_Tests.Test();

            //SecurityCryptography_Tests.ToHashSha256_Test();
            //ThreadTasksTest.LockObj_Test().Wait();
            //InjectionTest.HttpClient_Test(provider).Wait();

            //StronglyTypedEnumTests.Test1();
            ThreadTasksTest.ContinumeWithAsync_Test().Wait();
        }

        private async Task ValueTupleTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            //cts.Cancel();
            cts.Token.ThrowIfCancellationRequested();

            ValueTuple<Guid, Guid, string, bool> vt = new(Guid.NewGuid(), Guid.Empty, "yyy", true);

            List<ValueTuple<Guid, Guid, string, bool>> list = new();
            //List<(Guid TPPrcsId, Guid PrcsOptId, string Desc, bool result)> list = new();
            (Guid TPPrcsId, Guid PrcsOptId, string Status, bool result) step = new() { Status = "xxx", result = true };
            (Guid TPPrcsId, Guid PrcsOptId, string Desc, bool result) step1 = new() { Desc = "xxx Desc" };

            list.Add(step);
            list.Add(step1);
            list.Add(vt);
        }
        private async Task QuartzCronExpressionTestAsync()
        {
            var bdt = DateTime.Parse("2024-01-10T00:45:33").ToUniversalTime();//.UtcNow;//
            var dt = QuartzCronExpressionTest.ToUTCfromCron(bdt, "0 0 5 * * ?");
            var dt1 = QuartzCronExpressionTest.ToUTCfromCron(dt.Value, "0 0 5 * * ?");
            var dt2 = QuartzCronExpressionTest.ToUTCfromCron(dt1.Value, "0 0 5 * * ?");
            var dt3 = QuartzCronExpressionTest.ToUTCfromCron(dt2.Value, "0 0 5 * * ?");
            var dt4 = QuartzCronExpressionTest.ToUTCfromCron(dt3.Value, "0 0 5 * * ?");
            var dt5 = QuartzCronExpressionTest.ToUTCfromCron(dt4.Value, "0 0 5 * * ?");
            var dt6 = QuartzCronExpressionTest.ToUTCfromCron(dt5.Value, "0 0 5 * * ?");
            
            var tdt = QuartzCronExpressionTest.ToUTCfromCron(bdt, "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdt1 = QuartzCronExpressionTest.ToUTCfromCron(tdt.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt2 = QuartzCronExpressionTest.ToUTCfromCron(tdt1.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt3 = QuartzCronExpressionTest.ToUTCfromCron(tdt2.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt4 = QuartzCronExpressionTest.ToUTCfromCron(tdt3.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt5 = QuartzCronExpressionTest.ToUTCfromCron(tdt4.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt6 = QuartzCronExpressionTest.ToUTCfromCron(tdt5.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt7 = QuartzCronExpressionTest.ToUTCfromCron(tdt6.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt8 = QuartzCronExpressionTest.ToUTCfromCron(tdt7.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt9 = QuartzCronExpressionTest.ToUTCfromCron(tdt8.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt10 = QuartzCronExpressionTest.ToUTCfromCron(tdt9.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt11 = QuartzCronExpressionTest.ToUTCfromCron(tdt10.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt12 = QuartzCronExpressionTest.ToUTCfromCron(tdt11.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt13 = QuartzCronExpressionTest.ToUTCfromCron(tdt12.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt14 = QuartzCronExpressionTest.ToUTCfromCron(tdt13.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt15 = QuartzCronExpressionTest.ToUTCfromCron(tdt14.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            var tdt16 = QuartzCronExpressionTest.ToUTCfromCron(tdt15.ToUniversalTime(), "0 */30 0-5 * * ?").Value.ToLocalTime();
            
            var tdtx = QuartzCronExpressionTest.ToUTCfromCron(bdt, "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdtx1 = QuartzCronExpressionTest.ToUTCfromCron(tdtx.ToUniversalTime(), "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdtx2 = QuartzCronExpressionTest.ToUTCfromCron(tdtx1.ToUniversalTime(), "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdtx3 = QuartzCronExpressionTest.ToUTCfromCron(tdtx2.ToUniversalTime(), "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdtx4 = QuartzCronExpressionTest.ToUTCfromCron(tdtx3.ToUniversalTime(), "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdtx5 = QuartzCronExpressionTest.ToUTCfromCron(tdtx4.ToUniversalTime(), "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdtx6 = QuartzCronExpressionTest.ToUTCfromCron(tdtx5.ToUniversalTime(), "0 0 0-5 * * ?").Value.ToLocalTime();
            var tdtx7 = QuartzCronExpressionTest.ToUTCfromCron(tdtx6.ToUniversalTime(), "0 0 0-5 * * ?").Value.ToLocalTime();
        }
        private async Task SynSugarTest()
        {
            HttpClient client = new HttpClient();
            try
            {
                var resp = await client.GetAsync("http://www.baidu.com");
            }
            catch (Exception ex)
            {

            }

            for (int ixe = 0; ixe < 10; ixe++)
            {
                int mm = unchecked((int)(int.MaxValue + ixe));
            }
            int i = 100_000_000;
            Console.WriteLine($"{i:N0}");
            (int? id, string namd) ix = new(3, "33");
            (int? id, string namd) ix1;
            ix1.id = 4;
            ix1.namd = "44";

            Tuple<int, string> tpl = new Tuple<int, string>(1, "1-1");

            ValueTuple<int, string, BaseOX> ss = new ValueTuple<int, string, BaseOX>(1, "1-1", null);
            ss.Item1 = 2;
            ss.Item2 = "2-2";

            var sv = rcd(ref ss);
            var sv1 = rxcdv(ss);

            ValueTuple<int, string, BaseOX> sts;
            sts.Item1 = 1;
            sts.Item2 = "1-1";
            sts.Item3 = new BaseOX { BaseOX_Name = "xxx" };
        }

        private (int, string, BaseOX) rxcdv((int, string, BaseOX) ss)
        {
            ss.Item1 = 4;
            ss.Item2 = "4-4";
            ss.Item3 = new BaseOX { BaseOX_Name = "BaseOX 4" };
            return ss;
        }
        private (int, string, BaseOX) rcd(ref (int, string, BaseOX) ss)
        {
            ss.Item1 = 3;
            ss.Item2 = "3-3";
            ss.Item3 = new BaseOX { BaseOX_Name = "BaseOX 3" };
            return ss;
        }

        private async Task FuncTest()
        {
            var v1 = new BaseOX { BaseOX_Name = "V1", Rec = 10 };
            var v2 = new BaseOX { BaseOX_Name = "V2" };
            var v3 = v1 + v2;
            var str = (string)v3;
            var str1 = v3.ToString();
            var vs1 = (BaseOXSep1)v1;
            BaseOXSep1 vs2 = v1;
        }

        private async Task XmlDeserializeTest()
        {
            //await XMLSchemaTest.Test1();
            //await XMLSchemaTest.Test2();
            //await XMLSchemaTest.Test3();
            //await XMLSchemaTest.Test4();
            await XMLSchemaTest.Test5();
        }

        private async Task JsonSerializerDeserializeTest()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "a", "va" },
                { "b", "vb" },
                { "c", "vc" }
            };
            var dicString = JsonConvert.SerializeObject(dic);
            var deserial = JsonConvert.DeserializeObject<Dictionary<string, string>> (dicString);



            var scx = new ExtClassModel();

            string xml = XMLConversionsHelper.SerializerToXML(scx);
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);
            string json = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented, true);


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var body = System.Text.Json.JsonSerializer.Deserialize<SimpleModel>(" ", options); //"{\"id\":5,\"Name\":\"22x\"}"
        }

        private async Task DictionaryTest()
        {
            var mc = new MemoryCache(new MemoryCacheOptions { });
            mc.Set("key1", "value1", new MemoryCacheEntryOptions() { });

            var dic1 = new Dictionary<string, string>() { { "1", "1-a" }, { "2", "2-a" }, { "3", "3-a" }, { "4", "4-a" }, { "5", "5-a" } };
            var dic2 = new Dictionary<string, string>() { { "21", "21-a" }, { "22", "22-a" }, { "23", "23-a" }, { "24", "24-a" }, { "25", "25-a" } };

            string key = "1";
            if (!dic1.TryGetValue(key, out var vl) && !dic2.TryGetValue(key, out vl))
            {
            }
        }

        private async Task InitialTest()
        {
            var tkm = new TKMEx { access_token = "atk", expires_in = 33, token_type = "ttp" };
            tkm.Item = "ds";
            tkm.item = "ds_item";
            tkm.BaseItem = "b_Item";
            tkm.TKMEx_Item = "TKMEx_Item_ex";
            tkm.xxItem = "xxID";

            var tkm1 = new TKMExx<TKME_Sub>
            {
                Infor = new TKME_Sub { xxItem = "idxx", Item = "sub item", TKMEx_Item = "v" }     //
                ,
                BaseItem = "b_Item",
                Item = "ds",
                TKMEx_Item = "TKMEx_Item_exx",
                xxItem = "xxID"
            };

            var str = XMLConversionsHelper.SerializerToXML(tkm1);
            var isXml = XMLConversionsHelper.IsXMLString(str);
            var isXml_f = XMLConversionsHelper.IsXMLString($"{str}<a>s</a>");
            var isXml_t = XMLConversionsHelper.IsXMLString($"<a>{str}</a>");

            var str1 = JsonConvert.SerializeObject(tkm).ToString();
            var str2 = JsonConvert.SerializeObject(str1, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }
    }
}
