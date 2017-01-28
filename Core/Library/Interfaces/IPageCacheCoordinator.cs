using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageCacheCoordinator
    {
        Page GetCachedOrNewPage(string key, IParametersService parameters = null);

        PageCacheContainer TryGetCachedPage(string key);

        Page GetNewPage(string key);

        void RemoveCachedPages(string key);

        void LoadCachedPages(string key, CacheOption option = CacheOption.None);

        void AddPageToCache(string key, PageMapContainer container, bool isInitialized = false);
    }
}
