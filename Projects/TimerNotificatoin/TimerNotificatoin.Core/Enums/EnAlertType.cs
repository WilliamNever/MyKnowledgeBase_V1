using TimerNotificatoin.Core.Attributes;

namespace TimerNotificatoin.Core.Enums
{
    [HelperOutput("enum EnAlertType - Indicate looping type for an alert")]
    public enum EnAlertType
    {
        [HelperOutput("None - not loop (0)")]
        None = 0,
        [HelperOutput("DAY - looping base on day (1)")]
        DAY = 1,
        [HelperOutput("WEEK - looping base on week (2)")]
        WEEK = 2,
        [HelperOutput("MONTH - looping base on month (3)")]
        MONTH = 3,
        [HelperOutput("YEAR - looping base on year (4)")]
        YEAR = 4,
    }
}
