using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNotificatoin.Core.Enums
{
    [Flags]
    public enum EnMessageType
    {
        None = 0,
        MessageShow = 1,
        StatusShow = 2,
        NotificationShow = 4,
        Stopped = 8,
        Started = 16,
    }
}
