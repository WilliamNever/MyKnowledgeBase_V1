using F8C.Core.Enums;
using F8C.Core.Models;
using System.ComponentModel.Design;

namespace F8C.Core.Interfaces
{
    public interface ICreate8Method
    {
        Create8Method Identity { get; }
        string Name { get; }

        TService? GetService<TService>() where TService : class;
    }
}
