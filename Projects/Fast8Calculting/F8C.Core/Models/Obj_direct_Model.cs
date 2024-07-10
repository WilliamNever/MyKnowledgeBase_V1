using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F8C.Core.Models
{
    public class Obj_direct_Model : BaseMethodModel
    {
        public int ObjSymbol { get; set; }
        public int Direct { get; set; }
        public int TimeHH12 { get; set; }
        public override int First { get => ObjSymbol; set => ObjSymbol = value; }
        public override int Second { get => Direct; set => Direct = value; }
        public override int RateNum => First + Second + TimeHH12;
    }
}
