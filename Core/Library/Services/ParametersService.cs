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

        public virtual bool TryAdd(object item)
        {
            return TryAdd(item.GetType().Name, item);
        }

        public virtual T TryGet<T>(string key)
        {
            object value;
            if (Store.TryGetValue(key, out value))
            {
                return (T)value;
            }
            return default(T);
        }

        public virtual T TryGet<T>()
        {
            return TryGet<T>(typeof(T).Name);
        }

        public virtual bool TryRemove(string key)
        {
            return Store.Remove(key);
        }

        public virtual bool TryRemove<T>()
        {
            return TryRemove(typeof(T).Name);
        }

        public virtual IDictionary<string, object> GetDictionary()
        {
            return Store;
        }
    }
}
