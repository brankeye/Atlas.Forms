using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Interfaces.Components
{
    public interface ICacheController
    {
        void AddPage(string key, PageCacheContainer container);

        object TryGetCachedPage(string key, IParametersService parameters);

        void RemoveCachedPages(string key);

        void AddCachedPages(IList<PageCacheContainer> list);
    }
}
