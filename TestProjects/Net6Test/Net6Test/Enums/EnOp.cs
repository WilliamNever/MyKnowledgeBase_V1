using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.Enums
{
    [Flags]
    public enum EnOp
    {
        None = 0,
        A = 1,
        B = 2,
        A_B = 3,
        C = 4,
        D = 8
    }
}
