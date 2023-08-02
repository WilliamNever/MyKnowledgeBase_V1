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
        /// to fit 3 or more continuous special charactors
        /// </summary>
        public Regex RegexMultiSpecLetters { get; set; } = new Regex(@"([a-z0-9A-Z])\1{2,}");

        public RegexesTestService()
        {
        }

        public bool RegexMultiSpecLetters_Test(string str)
        { 
            var isMatch = RegexMultiSpecLetters.IsMatch(str);
            var matches = RegexMultiSpecLetters.Matches(str).ToList();
            return isMatch;
        }
    }
}
