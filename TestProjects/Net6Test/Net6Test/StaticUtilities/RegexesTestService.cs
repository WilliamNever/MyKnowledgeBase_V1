using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Net6Test.StaticUtilities
{
    public class RegexesTestService
    {
        /// <summary>
        /// Regex expression to fit 3 or more continuous special charactors
        /// </summary>
        public static Regex RegexMultiSpecLetters { get; set; } = new Regex(@"([a-z0-9A-Z])\1{2,}");
        public static Regex RegexMultiSpecLetters1 { get; set; } = new Regex(@"([a-z0-9A-Z])\1(cb)\2");
        public static Regex RegexMultiSpecLetters2 { get; set; } = new Regex(@"([a-z0-9A-Z])\1(cb)\1");

        public RegexesTestService()
        {
        }

        public void RegexMultiSpecLetters_Test()
        {
            var isMatch = RegexMultiSpecLetters.IsMatch("aaaacbcbbcc");
            var matches = RegexMultiSpecLetters.Matches("aaaacbcbbcc").ToList();

            var isMatch1 = RegexMultiSpecLetters1.IsMatch("aaaacbcbbcc");
            var matches1 = RegexMultiSpecLetters1.Matches("aaaacbcbbcc").ToList();

            var isMatch2 = RegexMultiSpecLetters2.IsMatch("aaaacbacbbcc");
            var matches2 = RegexMultiSpecLetters2.Matches("aaaacbacbbcc").ToList();
        }

        public static void RegexFileNameReplace_Test()
        {
            var errChars = @"\\/:*?""<>|";
            var regx = new Regex($"[{errChars}]");
            var str = @"c\\a/:*?""<>|";
            var dst = regx.Replace(str, "-");
        }
    }
}
