using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePreTest.Tests
{
    public static class SecTest
    {
        private static void OpenFolderSelectFiles(string pathFile)
        {
            var psi = new ProcessStartInfo("Explorer.exe")
            {
                Arguments = pathFile
                //WorkingDirectory =
            };
            //psi.FileName = pathFile;
            psi.WorkingDirectory = Path.GetDirectoryName(pathFile);
            //psi.CreateNoWindow = true;
            //psi.WindowStyle = ProcessWindowStyle.Maximized;

            using (var proc = Process.Start(psi))
            {
                proc.WaitForExit();
            }
        }

        public static void OpenTest1()
        {
            //OpenFolderSelectFiles("D:\\WQPersonal\\PrvCustomerTools\\TimerNotificatoin\\WQ-TimerNotificatoin.exe");
            OpenFolderSelectFiles("D:\\WQPersonal\\PrvCustomerTools\\TimerNotificatoin.lnk");
        }

        public static void DateTimeUtcConverTest()
        {
            var dt = DateTime.UtcNow;
            var dtToUtc = dt.ToUniversalTime();
            var kindDT = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            var toLcDt = dt.ToLocalTime();
            var kLcDt = kindDT.ToLocalTime();
        }
    }
}
