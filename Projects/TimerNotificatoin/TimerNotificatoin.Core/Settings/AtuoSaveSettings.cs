using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNotificatoin.Core.Settings
{
    public class AtuoSaveSettings
    {
        public double Interval { get; set; } = 5000;
        public bool AutoReset { get; set; }
    }
}
