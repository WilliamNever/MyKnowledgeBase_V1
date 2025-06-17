using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNotificatoin.Core.Interfaces
{
    public interface ICompareIdentity<T>
    {
        T Identity { get; }
    }
}
