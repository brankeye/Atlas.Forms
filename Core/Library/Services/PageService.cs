using System.Collections.Generic;
using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class PageService : IPageService
    {
        public static IPageService Current { get; internal set; }

        protected IPageCacheCoordinator CacheCoordinator { get; }

        protected INavigationProvider NavigationProvider { get; }

        public PageService(IPageCacheCoordinator cacheCoordinator, INavigationProvider navigationProvider)
        {
            CacheCoordinator = cacheCoordinator;
            NavigationProvider = navigationProvider;
        }

        public IReadOnlyDictionary<string, PageCacheContainer> CachedPages => PageCacheStore.Current.GetPageCache();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap => PageCacheMap.Current.GetMappings();

        public virtual Page GetCachedOrNewPage(string key, IParametersService parameters = null)
        {
            var page = CacheCoordinator.TryGetCachedPage(key);
            NavigationProvider.TrySetNavigation(page);
            return page.Page;
        }

        public virtual Page TryGetCachedPage(string key, IParametersService parameters = null)
        {
            //return CacheCoordinator.TryGetCachedPage(key);
            return null;
        }

        public virtual void AddPage(string key, PageMapContainer container, bool isInitialized = false)
        {
            CacheCoordinator.AddPageToCache(key, container, isInitialized);
        }
    }
}
