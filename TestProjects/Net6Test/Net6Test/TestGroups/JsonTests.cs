using Net6Test.Models;
using Newtonsoft.Json.Linq;

namespace Net6Test.TestGroups
{
    public class JsonTests
    {
        public static string jsStr = @"
{
  ""condition"": ""AND"",
  ""rules"": [
    {
      ""id"": ""price"",
      ""field"": ""price"",
      ""type"": ""double"",
      ""input"": ""number"",
      ""operator"": ""less"",
      ""value"": [10.25]
    },
    {
      ""condition"": ""OR"",
      ""rules"": [
        {
          ""id"": ""category"",
          ""field"": ""category"",
          ""type"": ""integer"",
          ""input"": ""select"",
          ""operator"": ""equal"",
          ""value"": [2]
        },
        {
          ""id"": ""category"",
          ""field"": ""category"",
          ""type"": ""integer"",
          ""input"": ""select"",
          ""operator"": ""equal"",
          ""value"": [1]
        }
      ]
    }
  ],
  ""valid"": true
}
";

        public static async Task JsTestAsync()
        {
            var js = JObject.Parse(jsStr).ToObject<QueryBuilderRuleModel>();
            var lst = js.rules.Where(x => x.rules == null).ToList();
            var lst1 = js.rules.Where(x => x.rules != null).SelectMany(x => x.rules).ToList();
        }
    }
}
