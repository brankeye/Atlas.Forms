using System.Collections.Generic;
using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Services
{
    public class ParametersService : IParametersService
    {
        protected IDictionary<string, object> Store { get; set; } = new Dictionary<string, object>();

        public virtual bool TryAdd(string key, object item)
        {
            var canAddItem = item != null && !Store.ContainsKey(key);
            if (canAddItem)
            {
                Store[key] = item;
            }
            return canAddItem;
        }

        public virtual TObject TryGet<TObject>(string key)
        {
            object value;
            if (Store.TryGetValue(key, out value))
            {
                return (TObject)value;
            }
            return default(TObject);
        }

        public virtual bool TryRemove(string key)
        {
            return Store.Remove(key);
        }
        
        public virtual IDictionary<string, object> GetDictionary()
        {
            return Store;
        }
    }
}
