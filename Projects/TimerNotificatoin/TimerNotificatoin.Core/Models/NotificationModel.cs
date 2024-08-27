using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNotificatoin.Core.Models
{
    public class NotificationModel
    {
        public Guid Id { get; set; }

        public DateTime AlertDateTime { get; set; }
        public long LeftSecones { get; set; }
        public bool IsAlerted { get; set; } = false;

        public string Title { get; set; }
        public string Description { get; set; }

        public NotificationModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
