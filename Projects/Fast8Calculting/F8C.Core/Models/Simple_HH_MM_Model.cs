using F8C.Core.Enums;

namespace F8C.Core.Models
{
    public class Simple_HH_MM_Model : BaseMethodModel
    {
        public override int RateNum { get => First + Second; set => throw new NotSupportedException(); }

        public override Create8Method Identity => Create8Method.Simple_HH_MM;
    }
}
