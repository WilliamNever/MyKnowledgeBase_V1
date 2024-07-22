using F8C.Core.Enums;

namespace F8C.Core.Models
{
    public class Result8SymbolModel
    {
        public Result8SymbolModel(Create8Method methodIdentity)
        {
            Identity = methodIdentity;
        }
        public Create8Method Identity { get; protected set; }
        public int Begin { get; set; }
        public int Changed { get; set; }
        public int Exchange { get; set; }
    }
}
