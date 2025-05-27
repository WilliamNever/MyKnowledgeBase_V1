using Net6Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.TestGroups
{
    public class LogicShema_Tests
    {
        public static void Test1()
        {
            Base1 base1 = new Base1();
            base1.ShowId("info");
        }
    }
}
