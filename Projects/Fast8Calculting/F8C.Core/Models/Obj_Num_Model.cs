using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F8C.Core.Models
{
    public class Obj_Num_Model : BaseMethodModel
    {
        public int ObjNum { get; set; }
        public int TimeHH12 { get; set; }
        public override int First { get => ObjNum; set => ObjNum = value; }
        public override int Second { get => TimeHH12; set => TimeHH12 = value; }
        public override int RateNum { get => First + Second; set => throw new NotSupportedException(); }
    }
}
