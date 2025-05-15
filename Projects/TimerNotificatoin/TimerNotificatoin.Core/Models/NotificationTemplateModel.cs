using TimerNotificatoin.Core.Attributes;

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
    public class NotificationTemplateModel
    {
        [HelperOutput("Guid Id - The identity of the alert")]
        public Guid Id { get; set; }
        [HelperOutput("string Name - The name of the Notification Template")]
        public string Name { get; set; }
        [HelperOutput("string Descriptions - The Descriptions of the Notification Template")]
        public string Descriptions { get; set; }
        [HelperOutput("string CronoExp - Crono expression - https://github.com/HangfireIO/Cronos")]
        public string CronoExp { get; set; }
    }
}
