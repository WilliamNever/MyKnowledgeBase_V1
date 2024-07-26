using F8C.Core.Enums;

namespace F8C.Core.Models
{
    public class Obj_direct_Model : BaseMethodModel
    {
        public int ObjSymbol { get; set; }
        public int Direct { get; set; }
        public int TimeHH12 { get; set; }
        public override int First { get => ObjSymbol; set => ObjSymbol = value; }
        public override int Second { get => Direct; set => Direct = value; }
        public override int RateNum { get => First + Second + TimeHH12; set => throw new NotSupportedException(); }

        public override Create8Method Identity => Create8Method.Obj_direct;
    }
}
