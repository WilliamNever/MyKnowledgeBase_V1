using F8C.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartTest
{
    public class TestPart1
    {
        public static void Get12ByDateTime()
        {
            int num = 24;
            DateTime dt = DateTime.Now.Date;
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine($"{i} - {ConstDefine.GetDT12(dt.AddHours(i))}");
            }
        }
        public static void Get12ByInt()
        {
            int num = 24;
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine($"{i} - {ConstDefine.GetDT12(i)}");
            }
        }

        public static (int s, int h, int d) Create8Result(int v1, int v2)
        {
            var s = ConstDefine.Dics[v1 & ConstDefine.Full3Positive] << 3
                  | ConstDefine.Dics[v2 & ConstDefine.Full3Positive];
            var h = (s >> 2 & ConstDefine.Full3Positive) << 3 | (s >> 1 & ConstDefine.Full3Positive);
            var c = ConstDefine.BitNums[(v1 + v2) % 6];
            var d = s ^ c;
            return (s,h,d);
        }
    }
}
