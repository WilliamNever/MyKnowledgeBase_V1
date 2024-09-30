using TimerNotificatoin.Core.Attributes;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput(@"class NotificationTemplateModel - Inherited from NotificationModel")]
    public class NotificationTemplateModel : NotificationModel
    {
        [HelperOutput("TemplateAlertDateModel AlertDateTemplate - Loop Alert Date Time definition.")]
        public TemplateAlertDateModel AlertDateTemplate { get; set; } = null!;
        [HelperOutput("DateTime AlertDateTime - Alert Date Time. According to EnAlertType, keep the HH:mm")]
        public override DateTime AlertDateTime { 
            get => AlertDateTemplate.AlertDateTime;
            set => AlertDateTemplate.AlertDateTime = value;
        }
        public NotificationTemplateModel():base()
        {
            
        }
    }
}
