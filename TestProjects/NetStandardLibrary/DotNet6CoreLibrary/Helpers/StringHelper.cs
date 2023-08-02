using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNet6CoreLibrary.Helpers
{
    public static class StringHelper
    {
        public const string SeparatedString = "@Separator@";
        public const string DoubleSlashReplace = "@DoubleSlash@";
        public static string[] ToCSVArray(this string csv, string separatedChar = ",")
        {
            if (separatedChar == "@") throw new Exception("The separated Char cannot be '@'.");

            Regex regSlash = new Regex($"[\"]+");
            Regex reg2Slash = new Regex("[\"]{2}");
            bool switcher = true;
            var str = regSlash.Replace(csv, m =>
            {
                var tmp = "";
                if (switcher)
                {
                    //var matches = reg2Slash.Matches(m.Value);
                    tmp = reg2Slash.Replace(m.Value, DoubleSlashReplace, int.MaxValue, 1);
                }
                else
                {
                    tmp = reg2Slash.Replace(m.Value, DoubleSlashReplace);
                }
                if ((m.Value.Length & 1) == 1) switcher = !switcher;
                return tmp;
            });
            str = reg2Slash.Replace(str, DoubleSlashReplace);

            Regex regComp = new("\"[\\s\\S]+?\"");
            str = regComp.Replace(str, m =>
            {
                return m.Value.Replace(separatedChar, SeparatedString);
            });
            var array = str.Split(separatedChar);
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Replace(DoubleSlashReplace, "\"\"");
                array[i] = array[i].Replace(SeparatedString, separatedChar);
            }
            return array;
        }
    }
}
