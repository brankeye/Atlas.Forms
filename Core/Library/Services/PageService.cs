using System.Collections.Generic;
using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class PageService : IPageService
    {
        public static IPageService Current { get; internal set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageCacheController PageCacheController { get; }

        public PageService(INavigationController navigationController, IPageCacheController pageCacheController)
        {
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public IReadOnlyDictionary<string, PageCacheContainer> CachedPages => PageCacheStore.Current.GetPageCache();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap => PageCacheMap.Current.GetMappings();

        public virtual Page GetCachedOrNewPage(string key, IParametersService parameters = null)
        {
            var page = PageCacheController.GetCachedOrNewPage(key, parameters) as Page;
            //NavigationProvider.TrySetNavigation(page);
            return page;
        }

        public virtual Page TryGetCachedPage(string key, IParametersService parameters = null)
        {
            //return CacheCoordinator.TryGetCachedPage(key);
            return null;
        }

        public virtual void AddPage(string key, PageMapContainer container, bool isInitialized = false)
        {
            //PageCacheController.AddPageToCache(key, container, isInitialized);
        }
    }
}
