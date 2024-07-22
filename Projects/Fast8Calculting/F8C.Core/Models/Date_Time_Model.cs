using F8C.Core.Enums;

namespace F8C.Core.Models
{
    public class Date_Time_Model : BaseMethodModel
    {
        public int NgYear { get; set; }
        public int NgMonth { get; set; }
        public int NgDay { get; set; }
        public int TimeHH12 { get; set; }

        public override int First { get => NgYear + NgMonth + NgDay; set => throw new NotSupportedException(); }
        public override int Second { get => NgYear + NgMonth + NgDay + TimeHH12; set => throw new NotSupportedException(); }
        public override int RateNum { get => Second; set => throw new NotSupportedException(); }

        public override Create8Method Identity => Create8Method.Date_Time;
    }
}
