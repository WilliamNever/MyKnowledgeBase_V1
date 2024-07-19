using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F8C.Core.Enums
{
    [Flags]
    public enum Create8Method
    {
        None = 0,
        Simple_HH_MM = 1,
        Date_Time = 2,
        Obj_direct = 4,
        Obj_Num = 8,
    }
}
