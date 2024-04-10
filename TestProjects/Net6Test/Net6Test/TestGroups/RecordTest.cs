using StandardLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class RecordTest
    {
        public static async Task Test1Async()
        {
            var obj = new CObjct("fn", "ln", 20) { InfoMessage = "information" };
            obj.InfoMessage = "xxx";

            var xmlString = XMLConversionsHelper.SerializerToXML(obj);
            var jsString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var de_Json = Newtonsoft.Json.JsonConvert.DeserializeObject<CObjct>(jsString);
            var isEqual = obj == de_Json;
        }
    }

    public record CObjct(string? Name, string? LastName, int? Age)
    {
        public CObjct() : this(null, null, null) { }
        public string? InfoMessage { get; set; }
    }
}
