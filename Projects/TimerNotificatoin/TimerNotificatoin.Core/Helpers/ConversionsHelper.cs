using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace TimerNotificatoin.Core.Helpers
{
    public static class ConversionsHelper
    {
        public static T? DeepCopy<T>(object obj) where T : class
        {
            return DeserializeJson<T>(SerializeToJson(obj), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public static string SerializeToCamelCaseJson(object obj)
        {
            return SerializeToJson(obj,
                new JsonSerializerOptions(JsonSerializerDefaults.General)
                { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                );
        }
        public static string NJ_SerializeToJson(object? obj, Newtonsoft.Json.JsonSerializerSettings? settings = null)
        {
            settings ??= new Newtonsoft.Json.JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, settings);
        }
        public static T? NJ_DeserializeToJson<T>(string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }
        public static string SerializeToJson(object obj, JsonSerializerOptions? jsOption = null)
        {
            if (jsOption == null)
                return JsonSerializer.Serialize(obj);
            else
                return JsonSerializer.Serialize(obj, jsOption);
        }
        public static T? DeserializeJson<T>(string str, JsonSerializerOptions? jsOption = null)
        {
            T? result;
            try
            {
                if (jsOption == null)
                    result = JsonSerializer.Deserialize<T>(str);
                else
                    result = JsonSerializer.Deserialize<T>(str, jsOption);
            }
            catch (Exception)
            {
                result = default;
            }
            return result;
        }

        public static T? ConvertToEnum<T>(string str, bool ignoreCases = true) where T : struct
        {
            T? resl = null;
            if (Enum.TryParse(str, ignoreCases, out T re))
            {
                resl = re;
            }
            return resl;
        }
        public static string SerializerToXML<T>(T model)
        {
            XmlWriterSettings XWS = new XmlWriterSettings();
            XWS.OmitXmlDeclaration = true;
            XWS.Indent = true;
            XWS.NamespaceHandling = NamespaceHandling.Default;
            XWS.Encoding = Encoding.UTF8;
            return SerializerToXML(model, XWS);
        }
        public static string SerializerToXML<T>(T model, XmlWriterSettings XWS)
        {
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww, XWS))
                {
                    XmlSerializer xSerial = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces nsx = new XmlSerializerNamespaces();
                    nsx.Add("", "");
                    xSerial.Serialize(writer, model, nsx);
                    writer.Flush();
                    sww.Flush();
                    return sww.ToString();
                }
            }
        }
        public static T? DeSerializerXML<T>(string str)
        {
            XmlSerializer serializer = new(typeof(T));
            T? rValue = default;
            StringReader sr;
            using (sr = new StringReader(str.Trim()))
            {
                var obj = serializer.Deserialize(sr);
                rValue = (T?)obj;
            }
            return rValue;
        }

        public static string SerializerToXMLWithNameSpace<T>(T model, string? ns = null)
        {
            using var sww = new Utf8StringWriter();
            XmlSerializer xSerial;
            if (string.IsNullOrEmpty(ns))
                xSerial = new XmlSerializer(typeof(T));
            else
                xSerial = new XmlSerializer(typeof(T), ns);
            xSerial.Serialize(sww, model);
            sww.Flush();
            return sww.ToString();
        }
        public static Dictionary<TKey, TValue>? AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value) where TKey : notnull
        {
            if (dic != null)
                if (dic.ContainsKey(key))
                    dic[key] = value;
                else
                    dic.Add(key, value);
            return dic;
        }
        public static void ReflectCopy<T>(T objA, ref T objB)
        {
            if (objA == null || objB == null) return;

            var clType = typeof(T);
            var aPropties = objA.GetType().GetProperties();
            foreach (var prop in aPropties)
            {
                clType.GetProperty(prop.Name)?.SetValue(objB, prop.GetValue(objA));
            }
        }
        public static string? GetPropertyStringValue(string? json, string? propertyName)
        {
            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(propertyName)) return null;

            var nJsn = Newtonsoft.Json.Linq.JToken.Parse(json);
            var retValue = nJsn.Value<string>(propertyName);

            return retValue;
        }
        public static Dictionary<string, StringCollection> ToDictionaryAttributes(
            this string str, string seperatedChar = " ", string setValueChar = "=")
        {
            var dic = new Dictionary<string, StringCollection>();
            if (str != null)
            {
                var parts = str.Split(seperatedChar, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var item in parts)
                {
                    var keyVal = item.Split(setValueChar);
                    var key = keyVal?.FirstOrDefault();
                    var val = keyVal != null && keyVal.Length > 1 ? keyVal[1].Trim('"').Trim('\'') : null;

                    if (key != null)
                    {
                        if (dic.TryGetValue(key, out var dicItem))
                        {
                            if (val != null) dicItem.Add(val);
                        }
                        else
                        {
                            dicItem = new StringCollection();
                            if (val != null) dicItem.Add(val);
                            dic.Add(key, dicItem);
                        }
                    }
                }
            }
            return dic;
        }
        public static string GenerateHash(string input)
        {
            using var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hexString = new StringBuilder();

            foreach (byte b in data) hexString.Append(b.ToString("x2"));

            return hexString.ToString();
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

}
