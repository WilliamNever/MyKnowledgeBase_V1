using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.Enums
{
    public enum EnWeek
    {
        Mon,
        Tue,
        Web,
        Thue,
        Fri,
        Sat,
        Sun
    }

    public static class EnWeekExtensions
    {
        public static EnWeek? GetWeekDay(this string mon)
        {
            return ConvertToEnum<EnWeek>(mon);
        }
        private static T? ConvertToEnum<T>(string str, bool ignoreCases = true) where T : struct
        {
            T? resl = null;
            if (Enum.TryParse(str, ignoreCases, out T re))
            {
                resl = re;
            }
            return resl;
        }
    }
}
