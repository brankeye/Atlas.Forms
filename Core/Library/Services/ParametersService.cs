using System.Collections.Generic;
using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Services
{
    public class ParametersService : IParametersService
    {
        protected Dictionary<string, object> Store { get; set; }

        public bool TryAdd(string key, object item)
        {
            if (item == null || Store.ContainsKey(key)) return false;
            Store.Add(key, item);
            return true;
        }

        public TObject TryGet<TObject>(string key)
        {
            object value;
            if (Store.TryGetValue(key, out value))
            {
                return (TObject)value;
            }
            return default(TObject);
        }

        public bool TryRemove(string key)
        {
            return Store.Remove(key);
        }
    }
}
