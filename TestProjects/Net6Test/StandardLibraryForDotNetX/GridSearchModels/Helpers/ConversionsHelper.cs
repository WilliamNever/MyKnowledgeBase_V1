
using Newtonsoft.Json;

namespace GridSearchModels.Helpers
{
    public static class ConversionsHelper
    {
        public static T? DeepCopy<T>(T obj) where T : class
        {
            return Deserialize<T>(Serialize(obj));
        }
        public static string Serialize<T>(T obj, JsonSerializerSettings? settings = null)
        {
            settings ??= new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.SerializeObject(obj, settings);
        }
        public static T? Deserialize<T>(string str) where T : class
        {
            T? result;
            try
            {
                result = JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception)
            {
                result = null;
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
        public static bool ToEquals(this string? str1, string? str2, bool IgnoreCases = true)
        {
            if (str1 == null) return str1 == str2;
            if (IgnoreCases)
            {
                return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return str1.Equals(str2);
            }
        }
        public static string? ReplaceStr(this string? str, string str1, string str2 = ""
            , StringComparison defaultComparison = StringComparison.OrdinalIgnoreCase)
        {
            return str?.Replace(str1, str2, defaultComparison);
        }
        public static TV? GetValueOrDefault<TK, TV>(this KeyValuePair<TK, TV> kv, TV? defaultValue = default)
        {
            return EqualityComparer<TK>.Default.Equals(kv.Key, default) ? defaultValue : kv.Value;
        }
    }
}
