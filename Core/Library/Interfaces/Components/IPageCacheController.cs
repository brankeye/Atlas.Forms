using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Atlas.Forms.Services;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IPageCacheController
    {
        object GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters);

        object TryGetCachedPage(string key, IParametersService parameters);

        object GetNewPage(NavigationInfo pageInfo);

        bool TryAddCachedPage(NavigationInfo pageInfo, CacheState cacheState);

        void AddCachedPages(string key);

        void AddCachedPagesWithOption(string key, CacheOption cacheOption);

        IList<PageMapContainer> GetMapContainers(string key);

        IList<PageMapContainer> GetMapContainersWithOption(string key, CacheOption cacheOption);

        void AddCacheContainers(string key, IList<PageCacheContainer> list);

        void RemoveCachedPages(string key);

        bool RemovePageFromCache(string key);
    }
}
