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
    }
}
