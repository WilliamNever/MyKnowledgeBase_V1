using System.Reflection;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Net6Test.Models
{
    public class TKModel : BaseStringIndex
    {
        public string? Item { get; set; }
        public string item { get; set; }
        public string access_token { get; set; }
        /// <summary>
        /// The length of time (in seconds) that the provided access token is valid for
        /// </summary>
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
    [XmlRoot( ElementName = "response" ,Namespace = "http://www.xxx.com/xxx/response", IsNullable = true)]
    public class TKMEx : TKModel
    {
        //[XmlAttribute(Namespace = XmlSchema.InstanceNamespace)]
        //public string xmlns;
        [XmlElement(ElementName = "id")]
        public new string? xxItem { get; set; }
        public new string? Item { get; set; }
        public string? TKMEx_Item { get; set; }
        public string? BaseItem { get { return base.Item; } set { base.Item = value; } }
    }

    public class BaseStringIndex
    {
        public int PV { get; set; }
        public string? GetPropertyValue(string key)
        {
            var property = GetType().GetProperty(key,
                BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Instance | BindingFlags.Static);
            return property?.GetValue(this)?.ToString();
        }
        public string? this[string key]
        {
            get
            {
                //return GetValue(key);
                //return GetValueFromJson(key);
                return GetValueFromJson1(key);
            }
       }

        private string? GetValueFromJson1(string key)
        {
            var js1 = System.Text.Json.JsonSerializer.Serialize(this, GetType());// as TKModel
            var dics1 = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(js1)!;
            var pv = dics1[key].ToString();
            return pv;
        }

        private string? GetValueFromJson(string key)
        {
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            var dics = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(js);
            return dics?.GetValueOrDefault(key);
        }

        private string? GetValue(string key)
        {
            var tp = GetType();
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x?.Name == key).ToList();
            var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var methods_pri = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            if (key == "Item")
            {
                var property = GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic
                    | BindingFlags.Instance | BindingFlags.Static)
                    .FirstOrDefault(x => x?.Name == key && x.DeclaringType?.Name != nameof(BaseStringIndex));
                return property?.GetValue(this)?.ToString();
            }
            else
            {
                var property = GetType().GetProperty(key,
                    BindingFlags.Public | BindingFlags.NonPublic
                    | BindingFlags.Instance | BindingFlags.Static);
                return property?.GetValue(this)?.ToString();
            }
        }
    }
}
