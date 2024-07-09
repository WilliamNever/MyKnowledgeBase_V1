using F8C.Core.Models;

namespace F8C.Core.Interfaces
{
    public interface ICreate8Method<T> where T : BaseMethodModel
    {
        Result8SymbolModel Get8Symbol(T model);
    }
}
