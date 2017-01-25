using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageCacheCoordinator
    {
        //IList<Page> GetSegmentPages(string key, IParametersService parameters = null);

        Page TryGetCachedPage(string key, IParametersService parameters = null);

        Page GetCachedOrNewPage(string key, IParametersService parameters = null);

        void RemoveCachedPages(string key);

        void LoadCachedPages(string key, CacheOption option = CacheOption.None);

        void AddPageToCache(string key, PageMapContainer container, bool isInitialized = false);
    }
}
