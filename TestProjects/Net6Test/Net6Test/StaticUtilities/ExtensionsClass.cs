using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.StaticUtilities
{
    public static class ExtensionsClass
    {
        private static IServiceProvider _provider;
        public static void Init(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
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
                            if (val != null)
                                dicItem.Add(val);
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
        public static T GetImplement<T>() where T : notnull
        {
            return _provider.GetRequiredService<T>();
        }
        public static string GetName(string name, string saying)
        {
            return $"{name} say/says {saying}!";
        }
        public static string ToUriEncode(this string? str)
        {
            return System.Web.HttpUtility.UrlEncode(str ?? "");
        }
    }
}
