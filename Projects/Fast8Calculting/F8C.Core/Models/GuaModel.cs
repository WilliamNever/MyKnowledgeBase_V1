using F8C.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace F8C.Core.Models
{
    public record GuaModel
    {
        public int Gua { get; private init; }
        public int Upper { get; private init; }
        public int Lower { get; private init; }
        public GuaModel(int up, int dl)
        {
            Upper = up;
            Lower = dl;
            Gua = up << 3 | dl;
        }
        public GuaModel(int gua)
        {
            Gua = gua;
            Upper = (Gua >> 3) & ConstDefine.Full3Positive;
            Lower = Gua & ConstDefine.Full3Positive;
        }
    }

    public record GuaNameModel(int Num, string Name, string SymbolName);
}
