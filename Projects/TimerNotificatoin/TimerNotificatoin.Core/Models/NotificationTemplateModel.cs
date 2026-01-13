using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Interfaces;

namespace TimerNotificatoin.Core.Models
{
    //[HelperOutput(@"class NotificationTemplateModel - Inherited from NotificationModel")]
    //public class NotificationTemplateModel : NotificationModel
    //{
    //    [HelperOutput("TemplateAlertDateModel AlertDateTemplate - Loop Alert Date Time definition.")]
    //    public TemplateAlertDateModel AlertDateTemplate { get; set; } = null!;
    //    [HelperOutput("DateTime AlertDateTime - Alert Date Time. According to EnAlertType, keep the HH:mm")]
    //    public override DateTime AlertDateTime { 
    //        get => AlertDateTemplate.AlertDateTime;
    //        set => AlertDateTemplate.AlertDateTime = value;
    //    }
    //    public NotificationTemplateModel():base()
    //    {

    //    }
    //}

    [HelperOutput(@"class NotificationTemplateModel - Template of Notification")]
    public class NotificationTemplateModel : ICompareIdentity<Guid>
    {
        Guid ICompareIdentity<Guid>.Identity { get => Id; }

        [HelperOutput("Guid Id - The identity of the alert")]
        public Guid Id { get; set; }
        [HelperOutput("string Name - The name of the Notification Template")]
        public string Name { get; set; }
        [HelperOutput("string Descriptions - The Descriptions of the Notification Template")]
        public string Descriptions { get; set; }
        [HelperOutput(@"string CronoExp - Crono expression - https://github.com/HangfireIO/Cronos

                cron ::= expression | macro
          expression ::= [second space] minute space hour space day-of-month space month space day-of-week
              second ::= field
              minute ::= field
                hour ::= field
        day-of-month ::= '*' [step] | '?' [step] | lastday | value [ 'W' | range [list] ]
               month ::= field
         day-of-week ::= '*' [step] | '?' [step] | value [ dowspec | range [list] ]
               macro ::= '@every_second' | '@every_minute' | '@hourly' | '@daily' | '@midnight' | '@weekly' | '@monthly'|
                         '@yearly' | '@annually'
               field ::= '*' [step] | '?' [step] | value [range] [list]
                list ::= { ',' value [range] }
               range ::= '-' value [step] | [step]
                step ::= '/' number
               value ::= number | name
                name ::= month-name | dow-name
          month-name ::= 'JAN' | 'FEB' | 'MAR' | 'APR' | 'MAY' | 'JUN' | 'JUL' | 'AUG' | 'SEP' | 'OCT' | 'NOV' | 'DEC'
            dow-name ::= 'SUN' | 'MON' | 'TUE' | 'WED' | 'THU' | 'FRI' | 'SAT'
             dowspec ::= 'L' | '#' number
             lastday ::= 'L' ['-' number] ['W']
              number ::= digit | number digit
               space ::= ' ' | '\t'


")]
        public string CronoExp { get; set; }
    }
}
