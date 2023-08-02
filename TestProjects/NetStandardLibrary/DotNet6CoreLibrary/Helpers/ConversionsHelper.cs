using DotNet6CoreLibrary.AttributeModels;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace DotNet6CoreLibrary.Helpers
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
                result = default(T);
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

        public static T? DeSerializerXML<T>(string str)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T? rValue = default(T);
            StringReader sr;
            using (sr = new StringReader(str.Trim()))
            {
                var obj = serializer.Deserialize(sr);
                rValue = (T?)obj;
            }
            return rValue;
        }

        public static string SerializerToXML<T>(T model, string? ns = null)
        {
            using (var sww = new Utf8StringWriter())
            {
                XmlSerializer xSerial;
                if (string.IsNullOrEmpty(ns))
                    xSerial = new XmlSerializer(typeof(T));
                else
                    xSerial = new XmlSerializer(typeof(T), ns);
                xSerial.Serialize(sww, model);
                sww.Flush();
                return sww.ToString();
            }
        }

        #region examples for Get special data via Custom Attribute/s
        private static CustomInListAttribute? GetCustomInListAttributeProperty(PropertyInfo? propInfor, string? batchName)
        {
            CustomInListAttribute? result = null;
            if (string.IsNullOrEmpty(batchName))
            {
                result = propInfor?.GetCustomAttributes<CustomInListAttribute>(true).FirstOrDefault(x => string.IsNullOrEmpty(x.BatchName));
            }
            else
            {
                result = propInfor?.GetCustomAttributes<CustomInListAttribute>(true).FirstOrDefault(x => batchName.Equals(x?.BatchName));
            }
            return result;
        }
        public static Dictionary<string, Dictionary<string, string?>?> ToUserImprints<T>(this string json, string? batchName = null)
        {
            var result = new Dictionary<string, Dictionary<string, string?>?>();
            var tClasss = DeserializeJson<T>(json);
            var prpties = typeof(T).GetProperties().Where(p => GetCustomInListAttributeProperty(p, batchName) != null);

            foreach (var prop in prpties)
            {
                var MainKeyName = prop.Name;
                var ValueKeyName = prop.Name;
                var vle = prop.GetValue(tClasss)?.ToString();

                var attr = GetCustomInListAttributeProperty(prop, batchName);
                MainKeyName = string.IsNullOrEmpty(attr?.MappingMainKey) ? MainKeyName : attr.MappingMainKey;
                ValueKeyName = string.IsNullOrEmpty(attr?.MappingValueKey) ? MainKeyName : attr.MappingValueKey;

                if (result.ContainsKey(MainKeyName))
                {
                    result[MainKeyName] ??= new Dictionary<string, string?>();
                    result[MainKeyName]?.AddOrUpdate(ValueKeyName, vle);
                }
                else
                {
                    result.Add(MainKeyName, new Dictionary<string, string?>().AddOrUpdate(ValueKeyName, vle));
                }
            }
            return result;
        }
        public static Dictionary<string, string?> ToUserMemberData<T>(this string json, string? batchName = null)
        {
            var result = new Dictionary<string, string?>();
            var tClasss = DeserializeJson<T>(json);
            var prpties = typeof(T).GetProperties().Where(p => GetCustomInListAttributeProperty(p, batchName) != null);

            foreach (var prop in prpties)
            {
                var MainKeyName = prop.Name;
                var vle = prop.GetValue(tClasss)?.ToString();

                var attr = GetCustomInListAttributeProperty(prop, batchName);
                MainKeyName = string.IsNullOrEmpty(attr?.MappingMainKey) ? MainKeyName : attr.MappingMainKey;

                if (!result.ContainsKey(MainKeyName))
                {
                    result.Add(MainKeyName, vle);
                }
            }
            return result;
        }
        #endregion
        
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
    }
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
