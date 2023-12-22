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
            FuncTest().Wait();
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
