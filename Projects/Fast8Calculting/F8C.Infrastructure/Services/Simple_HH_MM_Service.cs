using F8C.Core.Consts;
using F8C.Core.Enums;
using F8C.Core.Interfaces;
using F8C.Core.Models;

namespace F8C.Infrastructure.Services
{
    public class Simple_HH_MM_Service : Base8MethodService, ICreate8Method<Simple_HH_MM_Model>
    {
        public override Create8Method Identity => Create8Method.Simple_HH_MM;
        public override string Name => "Create by HH and MM";

        public Result8SymbolModel Get8Symbol(Simple_HH_MM_Model model)
        {
            int bgn = ConstDefine.BeginRsl(model.First, model.Second);
            int chg = ConstDefine.ChangedRsl(bgn, model.RateNum);
            return new Result8SymbolModel
            {
                Begin = bgn,
                Changed = chg,
                Exchange = ConstDefine.ExchangeRsl(bgn, chg)
            };
        }
    }
}
