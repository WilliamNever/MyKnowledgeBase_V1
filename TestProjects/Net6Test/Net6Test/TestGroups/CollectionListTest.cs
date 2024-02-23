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
    }
}
