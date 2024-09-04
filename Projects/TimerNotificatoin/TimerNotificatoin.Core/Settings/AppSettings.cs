using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNotificatoin.Core.Settings
{
    public class AppSettings
    {
        public double Interval { get; set; } = 5000;
        public string Notifications { get; set; } = null!;
        public string Templates { get; set; } = null!;
    }
}
