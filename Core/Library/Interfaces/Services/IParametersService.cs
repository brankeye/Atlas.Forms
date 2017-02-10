using System.Collections.Generic;

namespace Atlas.Forms.Interfaces.Services
{
    public interface IParametersService
    {
        IDictionary<string, object> GetDictionary();

        bool TryAdd(string key, object item);

        bool TryAdd(object item);

        T TryGet<T>(string key);

        T TryGet<T>();

        bool TryRemove(string key);

        bool TryRemove<T>();
    }
}
