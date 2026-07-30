using Microsoft.Extensions.Options;

namespace Net8Library.KeyVaultValusRead
{
    public class KeyVaultValueDictionary : IOptions<KeyVaultValueDictionary>
    {
        private Dictionary<string, string?> _dictionary = new();

        public KeyVaultValueDictionary Value => this;
        public Dictionary<string, string?> GetBaseDictionary() => _dictionary;
        public KeyVaultValueDictionary SetBaseDictionary(Dictionary<string, string?> dictionary)
        {
            foreach (var itm in dictionary.AsEnumerable())
            {
                AddOrUpdate(itm.Key, itm.Value);
            }
            return this;
        }
        public KeyVaultValueDictionary AddOrUpdate(string key, string? value)
        {
            var upperKey = key.ToUpper();
            if (_dictionary.Keys.Any(k => k == upperKey))
                _dictionary[upperKey] = value;
            else
                _dictionary.Add(upperKey, value);
            return this;
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
