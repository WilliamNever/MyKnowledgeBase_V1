using System;
using System.Collections.Generic;
using System.Text;

namespace Core31TestProject.Interfaces
{
    public interface InheritInterfaces<T> where T : IContactor
    {
        string To { get; set; }
        T Contact { get; set; }
    }
    public interface IContactor
    {
        string Name { get; set; }
        int Age { get; set; }
    }
}
