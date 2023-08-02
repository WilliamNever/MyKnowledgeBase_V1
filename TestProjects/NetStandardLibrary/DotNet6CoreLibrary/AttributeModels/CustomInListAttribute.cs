using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6CoreLibrary.AttributeModels
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class CustomInListAttribute : Attribute
    {
        public string? BatchName { get; set; }
        public string? MappingMainKey { get; set; }
        public string? MappingValueKey { get; set; }
        public CustomInListAttribute(string mappingName) : this(mappingName, mappingName)
        {
        }
        public CustomInListAttribute(string MainKeyName, string ValueKeyName)
        {
            MappingMainKey = MainKeyName;
            MappingValueKey = ValueKeyName;
        }
        public CustomInListAttribute()
        {
        }
    }
}
