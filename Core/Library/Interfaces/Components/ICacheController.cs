using System.Collections.Generic;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Info;

namespace Atlas.Forms.Interfaces.Components
{
    public interface ICacheController
    {
        bool TryAddPage(string key, PageCacheInfo info);

        object TryGetCachedPage(string key, IParametersService parameters);

        void RemoveCachedPages(string key);

        void AddCachedPages(IList<PageCacheInfo> list);

        bool RemovePageFromCache(string key);
    }
}
