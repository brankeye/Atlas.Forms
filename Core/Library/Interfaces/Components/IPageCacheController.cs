using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Info;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IPageCacheController
    {
        object GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters);

        object TryGetCachedPage(string key, IParametersService parameters);

        object GetNewPage(NavigationInfo pageInfo);

        bool TryAddCachedPage(NavigationInfo pageInfo, CacheState cacheState);

        bool TryAddCachedPage(Page pageInstance, PageMapInfo mapInfo);

        void AddCachedPages(string key);

        void AddCachedPagesWithOption(string key, CacheOption cacheOption);

        IList<PageMapInfo> GetMapContainers(string key);

        IList<PageMapInfo> GetMapContainersWithOption(string key, CacheOption cacheOption);

        void AddCacheContainers(string key, IList<PageCacheInfo> list);

        void RemoveCachedPages(string key);

        bool RemovePageFromCache(string key);
    }
}
