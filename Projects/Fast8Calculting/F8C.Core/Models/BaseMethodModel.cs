using F8C.Core.Enums;

namespace F8C.Core.Models
{
    public abstract class BaseMethodModel
    {
        public abstract Create8Method Identity { get; }
        public virtual int First { get; set; }
        public virtual int Second { get; set; }
        public virtual int RateNum { get; set; }
    }
}
