using System.Collections.Generic;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Services
{
    public class ParametersService : IParametersService
    {
        protected Dictionary<string, object> Store { get; set; }

        public bool TryAdd(string key, object item)
        {
            var canAddItem = item != null && !Store.ContainsKey(key);
            if (canAddItem)
            {
                Store.Add(key, item);
            }
            return canAddItem;
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
