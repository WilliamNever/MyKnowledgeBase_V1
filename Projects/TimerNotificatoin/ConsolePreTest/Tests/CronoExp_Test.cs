using Cronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePreTest.Tests
{
    public static class CronoExp_Test
    {
        public static async Task CronoExpress_Test()
        {
            var dtOffSet = new DateTimeOffset(DateTime.Now);
            CronExpression expression = CronExpression.Parse("45 09 * * TUE,THU");
            var next = expression.GetNextOccurrence(dtOffSet, TimeZoneInfo.Local).Value.DateTime;//?.ToLocalTime();
        }
    }
}
