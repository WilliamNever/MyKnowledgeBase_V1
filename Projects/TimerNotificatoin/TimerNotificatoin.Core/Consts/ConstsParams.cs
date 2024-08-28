using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNotificatoin.Core.Consts
{
    public static class ConstsParams
    {
        public static string RootDirectory { get => Environment.CurrentDirectory; }
        public static string TemplatePath { get => $"{RootDirectory}\\Configs\\Templates.json"; }
        public static string AlertPath { get => $"{RootDirectory}\\Configs\\Alerts.json"; }
        public static string HelperFilePath { get => $"{RootDirectory}\\Configs\\ReadMe.txt"; }
    }
}
