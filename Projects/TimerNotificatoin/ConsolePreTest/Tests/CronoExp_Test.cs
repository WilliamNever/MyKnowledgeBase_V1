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
            //CronExpression expression = CronExpression.Parse("45 09 L-2W * *");
            CronExpression expression = CronExpression.Parse("45 09 * * *");
            var nextDTOff = expression.GetNextOccurrence(dtOffSet, TimeZoneInfo.Local).Value;
            var next = nextDTOff.DateTime;//?.ToLocalTime();
            var next1 = expression.GetNextOccurrence(nextDTOff, TimeZoneInfo.Local).Value.DateTime;//?.ToLocalTime();
        }
    }
}
