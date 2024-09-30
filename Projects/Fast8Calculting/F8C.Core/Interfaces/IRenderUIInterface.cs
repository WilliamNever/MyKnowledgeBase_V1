using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F8C.Core.Interfaces
{
    public interface IRenderUIInterface
    {
        void RenderUI(string str, string? barMessage = null);
        void RenderUIStatusBar(string? barMessage = null);
    }
}
