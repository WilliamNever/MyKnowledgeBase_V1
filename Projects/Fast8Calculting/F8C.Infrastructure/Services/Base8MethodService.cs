using F8C.Core.Enums;
using F8C.Core.Interfaces;
using F8C.Core.Models;

namespace F8C.Infrastructure.Services
{
    public abstract class Base8MethodService
    {
        public abstract Create8Method Identity { get; }
        public abstract string Name { get; }

        public T? GetMethodClass<T>() where T : Base8MethodService
        {
            return this as T;
        }
        public ICreate8Method<T>? GetIMethodInterface<T>() where T : BaseMethodModel
        {
            return this as ICreate8Method<T>;
        }
    }
}
