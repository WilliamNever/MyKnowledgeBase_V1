using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StandardLibrary
{
    /// <summary>
    /// There are several approaches to read property values from base class as the class shows.
    /// </summary>
    public abstract class BaseClass
    {
        public string GetPropertyValue(string key)
        {
            return GetValue(key);
            //return GetValueFromJson(key);
            //return GetValueFromJson1(key);
        }

        /// <summary>
        /// - Index is a property named 'Item', when reflecting the class.
        ///   Pay attention to distinguish 'Item' properties in reflecting an Index.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                return GetValue(key);
                //return GetValueFromJson(key);
                //return GetValueFromJson1(key);
            }
        }

        /// <summary>
        /// this part referenced System.Text.Json.JsonSerializer, the dll exists in .net6, but not in standard 2.1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //private string GetValueFromJson1(string key)
        //{
        //    var js1 = System.Text.Json.JsonSerializer.Serialize(this, this.GetType());
        //    var dics1 = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, System.Text.Json.JsonElement>>(js1)!;
        //    var pv = dics1[key].ToString();
        //    return pv;
        //}

        private string GetValueFromJson(string key)
        {
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            var dics = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(js);
            return dics?.GetValueOrDefault(key);
        }

        private string GetValue(string key)
        {
            if (key == "Item")
            {
                var property = GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic
                    | BindingFlags.Instance | BindingFlags.Static)
                    .FirstOrDefault(x => x?.Name == key && x.DeclaringType?.Name != nameof(BaseClass));
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
