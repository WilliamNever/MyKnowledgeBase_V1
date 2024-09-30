using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerNotificatoin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class HelperOutputAttribute : Attribute
    {
        public string Description { get; set; }
        public HelperOutputAttribute(string description)
        {
            Description = description;
        }
    }
}
