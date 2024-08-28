using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Enums;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput(@"record TemplateAlertDateModel - Indicated the day of notifying")]
    public record TemplateAlertDateModel
        (int Year, int Month, int Day, DayOfWeek[] WeekDays, EnSpecialDays SpecialDays = EnSpecialDays.None)
    {
        [HelperOutput("Int Year - Indicated Year")]
        public int Year { get; init; } = Year;
        [HelperOutput("Int Month - Indicated Month")]
        public int Month { get; init; } = Month;
        [HelperOutput("Int Day - Indicated Day")]
        public int Day { get; init; } = Day;
        [HelperOutput("DayOfWeek[] WeekDays - Indicated WeekDays, the first day is Sunday(0), last one is Sat.(6)")]
        public DayOfWeek[] WeekDays { get; init; } = WeekDays;
        [HelperOutput("EnSpecialDays SpecialDays - Indicated SpecialDays, default value is EnSpecialDays.None")]
        public EnSpecialDays SpecialDays { get; init; } = SpecialDays;
    }
}
