using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Test.Models
{
    public class DictionaryExtensionsModel : Dictionary<string, string>
    {
        public void AddKeyValue(string key, string value)
        {
            Add(key.ToUpper(), value);
        }
        public new string this[string key]
        {
            get
            {
                return base[key.ToUpper()];
            }
            set
            {
                var upperKey = key.ToUpper();
                if (Keys.Any(k => k == upperKey))
                {
                    base[upperKey] = value;
                }
                else
                {
                    Add(upperKey, value);
                }
            }
        }
    }

    public class DictionaryInsightEx
    {
        private Dictionary<string, string?> _dictionary = new();
        public void AddOrUpdate(string key, string? value)
        {
            var upperKey = key.ToUpper();
            if (_dictionary.Keys.Any(k => k == upperKey))
                _dictionary[upperKey] = value;
            else
                _dictionary.Add(key.ToUpper(), value);
        }
        public string? this[string key]
        {
            get => TryGet(key, out string? rls) ? rls : null;
            set => AddOrUpdate(key, value);

        }
        public bool ExistsKey(string key)
        {
            var upperKey = key.ToUpper();
            return _dictionary.Keys.Any(k => k == upperKey);
        }
        public bool TryGet(string key, out string? value)
        {
            var upperKey = key.ToUpper();
            return _dictionary.TryGetValue(upperKey, out value);
        }

        public bool Remove(string key)
        {
            var upperKey = key.ToUpper();
            if (_dictionary.Keys.Any(k => k == upperKey))
                return _dictionary.Remove(upperKey);
            else
                return true;
        }
        public void Clear() => _dictionary.Clear();
    }
}
