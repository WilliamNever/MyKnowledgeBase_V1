using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.BaseClassModels
{
    public abstract class TestCaseBaseModel
    {
        public virtual async Task ExcuteAsync()
        {
            ConsoleEndLine();
        }
        protected static void ConsoleEndLine()
        {
            Console.WriteLine();
            Console.WriteLine($"Finished .net Core Test! Any key to exit......");
            Console.ReadKey();
        }
    }
}
