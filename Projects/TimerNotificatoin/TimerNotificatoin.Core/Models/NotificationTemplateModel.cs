using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Enums;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput(@"class NotificationTemplateModel - Inherited from NotificationModel")]
    public class NotificationTemplateModel : NotificationModel
    {
        [HelperOutput("EnAlertType AlertType - Indicated the type of alert")]
        public EnAlertType AlertType { get; set; }
        [HelperOutput("bool IsEnabled - Indicated if the alert is enabled")]
        public bool IsEnabled { get; set; } = true;
        [HelperOutput("DateTime AlertDateTime - Alert Date Time. According to EnAlertType, keep the HH:mm")]
        public override DateTime AlertDateTime { get; set; }
        public NotificationTemplateModel():base()
        {
            
        }
    }
}
