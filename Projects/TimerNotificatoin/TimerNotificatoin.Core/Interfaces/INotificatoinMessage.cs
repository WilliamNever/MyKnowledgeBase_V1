using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Models;

namespace TimerNotificatoin.Core.Interfaces
{
    public interface INotificatoinMessage
    {
        void ShowMessage(string message, EnMessageType messageType);
        void ShowMessage(NotificationModel message, EnMessageType messageType);
    }
}
