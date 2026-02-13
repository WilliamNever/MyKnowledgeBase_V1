using Net6Test.StaticUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public static class UnFiddledTests
    {
        public static async Task KVP_FilterTest()
        {
            Dictionary<string, SqlDbType> dc = new Dictionary<string, SqlDbType>() {
                { "aa", SqlDbType.Text },
                { "", SqlDbType.NVarChar }
            };
            var kv = dc.FirstOrDefault(x => x.Key.ToEquals("Aa")).GetValueOrDefault(SqlDbType.Real);
            var kv1 = dc.FirstOrDefault(x => x.Key.ToEquals("Aaz")).GetValueOrDefault(SqlDbType.Real);
            var kv2 = dc.FirstOrDefault(x => x.Key.ToEquals("Aaz")).GetValueOrDefault();
        }
        public static TV? GetValueOrDefault<TK, TV>(this KeyValuePair<TK, TV> kv, TV? defaultValue = default)
        {
            return EqualityComparer<TK>.Default.Equals(kv.Key, default) ? defaultValue : kv.Value;
        }
    }
}
