using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Enums;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput(@"class TemplateAlertDateModel - Indicated the day of notifying")]
    public class TemplateAlertDateModel
    {
        [HelperOutput("DateTime AlertDateTime - Indicated the base alert day time")]
        public DateTime AlertDateTime { get; set; }
        [HelperOutput("EnAlertType AlertType - Indicated the type of alert")]
        public EnAlertType AlertType { get; set; }
        [HelperOutput("double IntervalStep - Indicated Interval Step base on looping Alert type, delfault (0)")]
        public double IntervalStep { get; set; } = 0D;
        [HelperOutput("EnSpecialDays SpecialDays - Indicated SpecialDays, default value is EnSpecialDays.None")]
        public EnSpecialDays SpecialDays { get; set; }

        [HelperOutput("DayOfWeek[] WeekDays - Indicated WeekDays, the first day is Sunday(0), last one is Sat.(6)")]
        public DayOfWeek[]? WeekDays { get; set; }
        [HelperOutput("bool IsEnabled - Indicated if the alert is enabled")]
        public bool IsEnabled { get; set; } = true;

        [HelperOutput("ctor TemplateAlertDateModel - TemplateAlertDateModel(DateTime AlertDateTime, EnAlertType AlertType, double IntervalStep, EnSpecialDays SpecialDays)")]
        public TemplateAlertDateModel(DateTime AlertDateTime, EnAlertType AlertType, double IntervalStep, EnSpecialDays SpecialDays)
        {
            this.AlertDateTime = AlertDateTime;
            this.AlertType = AlertType;
            this.IntervalStep = IntervalStep;
            this.SpecialDays = SpecialDays;
        }
    }
}
