using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class DateTime_Tests
    {
        public static async Task Ticker_Test()
        {
            var ticker = 1530216375388;
            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(ticker);
            var dtoMS = dateTime.ToUnixTimeMilliseconds();
            var dtt = dateTime.Ticks;
            var dtOff = new DateTimeOffset(dateTime.DateTime);
            var dtOff1 = new DateTimeOffset(dateTime.UtcDateTime);
            var dtts = dtOff1.ToUnixTimeMilliseconds();
        }
        public static async Task Test()
        {
            var NowDateTime = DateTimeOffset.Now;

            DateTimeOffset removeDateTime;
            if (!TimeSpan.TryParse("00:4:00", out TimeSpan removeTime))
            {
                removeTime = TimeSpan.FromHours(4d);
            }
            removeDateTime = NowDateTime.Date.Add(removeTime);
            if (removeDateTime < NowDateTime)
            {
                removeDateTime = removeDateTime.AddDays(1d);
            }
        }

        public static async Task DateTimeFormat_Test()
        {
            var udtc = DateTime.Now;
            Console.WriteLine($"{udtc:yyyy-MM-ddTHH:mm:sszzz}");
            Console.WriteLine($"{udtc:o}");
            Console.WriteLine(udtc.ToString("yyyy-MM-ddTHH:mm:sszzz"));
        }
    }
}
