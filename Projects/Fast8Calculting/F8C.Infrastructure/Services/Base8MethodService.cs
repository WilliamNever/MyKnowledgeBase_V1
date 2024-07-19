using F8C.Core.Consts;
using F8C.Core.Models;

namespace F8C.Infrastructure.Services
{
    public abstract class Base8MethodService<T> where T : BaseMethodModel
    {
        public virtual Result8SymbolModel Get8Symbol(T model)
        {
            int bgn = ConstDefine.BeginRsl(model.First, model.Second);
            int chg = ConstDefine.ChangedRsl(bgn, model.RateNum);
            return new Result8SymbolModel
            {
                Identity = model.Identity,
                Begin = bgn,
                Changed = chg,
                Exchange = ConstDefine.ExchangeRsl(bgn, chg)
            };
        }
    }
}
