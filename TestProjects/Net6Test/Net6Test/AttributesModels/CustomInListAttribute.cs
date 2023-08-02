using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.AttributesModels
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CustomInListAttribute : Attribute
    {
        public string? AttributeName { get; set; }
        public bool IsIncluded { get; set; } = false;
        public CustomInListAttribute()
        {
            IsIncluded = true;
        }
        public CustomInListAttribute(bool isIncluded)
        {
            IsIncluded = isIncluded;
        }
    }
}
