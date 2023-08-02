using System;
using System.Collections.Generic;
using System.Text;

namespace Core31TestProject.Models
{
    public class DimensionItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContextPath { get; set; }
        public int Products { get; set; }
    }

    public class IndexedDimensionItem
    {
        public string this[string key]
        {
            get {
                var val = GetType().GetProperty(key)?.GetValue(this);
                return val?.ToString();
            }
        }
    }

    public class ExValue: IndexedDimensionItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContextPath { get; set; }
        public int Products { get; set; }
    }
}
