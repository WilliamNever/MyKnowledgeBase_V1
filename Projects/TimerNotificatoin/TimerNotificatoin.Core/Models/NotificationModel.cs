using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNotificatoin.Core.Attributes;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput($"class NotificationModel - alert config model")]
    public class NotificationModel
    {
        [HelperOutput("Guid Id - The identity of the alert")]
        public Guid Id { get; set; }
        [HelperOutput("DateTime AlertDateTime - Alert Date Time")]
        public virtual DateTime AlertDateTime { get; set; }
        [HelperOutput("double LeftSeconds - The seconds left to notifing, it was auto-calculated according AlertDateTime.")]
        public double LeftSeconds { get; set; }
        [HelperOutput("bool IsAlerted - Indicated if the notification is popped up")]
        public bool IsAlerted { get; set; } = false;
        [HelperOutput("string Title - Indicated the title of the notification")]
        public string Title { get; set; }
        [HelperOutput("string Description - Indicated the Description of the notification")]
        public string Description { get; set; }

        public NotificationModel()
        {
            Id = Guid.NewGuid();
            AlertDateTime = DateTime.Now.Date;
        }
    }
}
