using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Interfaces.Components
{
    public interface ICacheController
    {
        bool TryAddPage(string key, PageCacheContainer container);

        object TryGetCachedPage(string key, IParametersService parameters);

        void RemoveCachedPages(string key);

        void AddCachedPages(IList<PageCacheContainer> list);

        bool RemovePageFromCache(string key);
    }
}
