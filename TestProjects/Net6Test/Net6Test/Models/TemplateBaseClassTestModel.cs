using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.Models
{
    public abstract class TemplateBaseClassTestModel<T>
    {
        public abstract T GetMethod();
    }
}
