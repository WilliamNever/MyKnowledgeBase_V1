using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StandardLibrary.Helpers;

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

        public static async Task Test2Async()
        {
            var obj = new CObjct("fn", "ln", 20) { InfoMessage = "information" };
            obj.InfoMessage = "xxx";
            var str = JsonConvert.SerializeObject(default(string));
            var jobj = JObject.Parse("{}");
        }

        public static async Task Test3Async()
        {
            var obj = new CObjct("fn", "ln", 20) { InfoMessage = "information" };
            var jsString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var de_Json = Newtonsoft.Json.JsonConvert.DeserializeObject<CObjct>(jsString);

            var xx = JsonConvert.DeserializeObject<CObjct>("{null}");
        }
    }

    public record CObjct(string? Name, string? LastName, int? Age)
    {
        public CObjct() : this(null, null, null) { }
        public string? InfoMessage { get; set; }
    }
}
