using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Caching.Memory;
using Net6Test.Models;
using Net6Test.StaticUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using StandardLibrary.Helpers;

namespace Net6Test.TestEntrances
{
    public class F3MainEntrance : EntranceBase
    {
        public override void MainRun()
        {
            InitialTest().Wait();
            //DictionaryTest().Wait();
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

            var tkm1 = new TKMExx<int> { Infor = 33, BaseItem= "b_Item", Item= "ds", TKMEx_Item= "TKMEx_Item_exx", xxItem= "xxID" };

            var str = XMLConversionsHelper.SerializerToXML(tkm1);    

            var str1 = JsonConvert.SerializeObject(tkm).ToString();
            var str2 = JsonConvert.SerializeObject(str1, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }
    }
}
