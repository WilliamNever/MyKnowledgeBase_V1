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
        public GuaModel Begin { get; set; }
        public GuaModel Destination { get; set; }
        public GuaModel Exchange { get; set; }
        /// <summary>
        /// the first Yao is 0
        /// </summary>
        public int ChangedYao { get; set; }
    }
}
