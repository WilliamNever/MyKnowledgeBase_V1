using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Net6Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class CollectionListTest
    {
        public async static Task DictionaryExtensionsModel_Test()
        {
            var dcEx = new DictionaryExtensionsModel();
            dcEx.AddKeyValue("ss", "aa");
            var deVle = dcEx["Ss"];

            DictionaryInsightEx dcIEx = new DictionaryInsightEx();
            dcIEx.AddOrUpdate("ss", "aa");
            var deVle1 = dcIEx["Ss"];
            dcIEx["xM"] = "xmVle";
            var deVle2 = dcIEx["Ss1"];
        }

        public async static Task NestListToExtraList_Test(IServiceProvider provider)
        {
            var oriList = new BSList
            {
                Name = "Base",
                Descriptions = new List<string> { "d1", "d2", "d3" }
            };
            var imap = provider.GetService<IMapper>();
            var list = oriList.Descriptions.Select(x => { var mp = new BSList { Name = oriList.Name, Description = x }; return mp; });
        }

        public class BSList
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public List<string> Descriptions { get; set; }
        }
    }
}
