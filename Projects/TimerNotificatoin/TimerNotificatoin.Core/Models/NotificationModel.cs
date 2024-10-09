using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput($"class NotificationModel - alert config model")]
    public class NotificationModel
    {
        [HelperOutput("Guid Id - The identity of the alert")]
        public Guid Id { get; set; }
        [HelperOutput("DateTime AlertDateTime - Alert Date Time")]
        public virtual DateTime AlertDateTime { get; set; }
        //[HelperOutput("DateTime StartDateTime - Start Date Time")]
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        public virtual DateTime StartDateTime { get; set; }
        //[HelperOutput("double LeftSeconds - The seconds left to notifing, it was auto-calculated according AlertDateTime.")]
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        public double LeftSeconds { get; set; }
        [HelperOutput("bool ToAlert - Indicated if the notification will pop up")]
        public bool ToAlert { get; set; } = true;
        [HelperOutput("string Title - Indicated the title of the notification")]
        public string Title { get; set; }
        [HelperOutput("string Description - Indicated the Description of the notification")]
        public string Description { get; set; }

        //[HelperOutput("EnNotificationType NotificationType - Indicated if the notification can be remained after alerted")]
        //public EnNotificationType NotificationType { get; set; } = EnNotificationType.Common;
        [HelperOutput("int ClassificationID - Indicated which Classification is applied")]
        public int ClassificationID { get; set; }
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        public EnNotificationType NotificationType
        {
            get =>
                HOSTServices.GetClassifications().FirstOrDefault(x => x.ID == ClassificationID)?.NotificationType 
                ?? new ClassificationModel().NotificationType;
        }
#if !Debug
        [Newtonsoft.Json.JsonIgnore]
#endif
        public string ClassificationName
        {
            get =>
                HOSTServices.GetClassifications().FirstOrDefault(x => x.ID == ClassificationID)?.Name 
                ?? new ClassificationModel().Name;
        }

        public NotificationModel()
        {
            Id = Guid.NewGuid();
            AlertDateTime = DateTime.Now.Date;
        }
    }
}
