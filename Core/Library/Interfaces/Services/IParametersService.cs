using System.Collections;
using System.Collections.Generic;

namespace Atlas.Forms.Interfaces.Services
{
    public interface IParametersService
    {
        IDictionary<string, object> GetDictionary();

        bool TryAdd(string key, object item);

        TObject TryGet<TObject>(string key);

        bool TryRemove(string key);
    }
}
