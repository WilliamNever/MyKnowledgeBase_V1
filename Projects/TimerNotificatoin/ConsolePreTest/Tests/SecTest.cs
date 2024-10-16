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
            var psi = new ProcessStartInfo()// (pathFile);// ("Explorer.exe")
            {
                //Arguments = pathFile
                //WorkingDirectory =
            };
            psi.FileName = pathFile;
            psi.WorkingDirectory = Path.GetDirectoryName(pathFile);
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Maximized;
            
            Process.Start(psi);
        }

        public static void OpenTest1()
        {
            OpenFolderSelectFiles("D:\\WQPersonal\\PrvCustomerTools\\TimerNotificatoin\\WQ-TimerNotificatoin.exe");
        }
    }
}
