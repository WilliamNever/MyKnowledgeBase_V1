using F8C.Core.Consts;
using F8C.Core.Models;

namespace F8C.Infrastructure.Services
{
    public class PlumEasyCalculateService : Base8MethodService
    {
        public Result8SymbolModel GetPlumEasyCalculate8Symbol<TM>(TM model) where TM : BaseMethodModel
        {
            int bgn = ConstDefine.BeginRsl(model.First, model.Second);
            int chg = ConstDefine.ChangedRsl(bgn, model.RateNum);
            int chYao = ConstDefine.GetChangeYao(model.RateNum);
            return new Result8SymbolModel(model.Identity)
            {
                Begin = new(bgn),
                Destination = new(chg),
                Exchange = new(ConstDefine.ExchangeRsl(bgn, chg)),
                ChangedYao = chYao == 0 ? 6 : chYao,
            };
        }
    }
}
