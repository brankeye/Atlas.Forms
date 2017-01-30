using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IPageCacheController
    {
        object GetCachedOrNewPage(string key, IParametersService parameters);

        object TryGetCachedPage(string key, IParametersService parameters);

        void AddCachedPages(string key);

        void AddCachedPagesWithOption(string key, CacheOption cacheOption);

        IList<PageMapContainer> GetMapContainers(string key);

        IList<PageMapContainer> GetMapContainersWithOption(string key, CacheOption cacheOption);

        void AddCacheContainers(string key, IList<PageCacheContainer> list);

        void RemoveCachedPages(string key);
    }
}
