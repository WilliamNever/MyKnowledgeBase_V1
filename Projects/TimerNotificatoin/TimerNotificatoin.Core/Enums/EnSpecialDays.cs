using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNotificatoin.Core.Attributes;

namespace TimerNotificatoin.Core.Enums
{
    [Flags] 
    [HelperOutput("[Flags]enum EnSpecialDays - Indicate the Special days")]
    public enum EnSpecialDays
    {
        [HelperOutput("None - No spec (0)")]
        None = 0,
        [HelperOutput("FirstDay - the First Day (1)")]
        FirstDay = 1,
        [HelperOutput("LastDay - the Last Day (2)")]
        LastDay = 2,
        [HelperOutput("FirstWeek - the First Week (4)")]
        FirstWeek = 4,
        [HelperOutput("LastWeek - the Last Week (8)")]
        LastWeek = 8,
        [HelperOutput("EveryWeek - Every Week (16)")] 
        EveryWeek = 16,
    }
}
