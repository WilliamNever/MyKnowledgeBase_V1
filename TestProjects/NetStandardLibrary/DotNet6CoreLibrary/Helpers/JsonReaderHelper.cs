using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6CoreLibrary.Helpers
{
    public static class JsonReaderHelper
    {
        public static TV GetValueFromJson<TV>(string jsonString, string Key, TV defaultValue)
        {
            var nJv = JToken.Parse(jsonString).SelectToken(Key)?.ToObject<JValue>();
            if (nJv == null) return defaultValue;
            var vle = nJv.Value<TV>();
            if (vle == null) return defaultValue;
            return vle;
        }
    }
}
