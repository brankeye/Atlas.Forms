using System.Collections.Generic;
using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Services
{
    public class PageCacheService : IPageCacheService
    {
        public static IPageCacheService Current { get; internal set; }

        protected IPageCacheCoordinator CacheCoordinator { get; }

        internal PageCacheService(IPageCacheCoordinator cacheCoordinator)
        {
            CacheCoordinator = cacheCoordinator;
        }

        public IReadOnlyDictionary<string, PageCacheContainer> CachedPages => PageCacheStore.GetPageCache();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap => PageCacheMap.GetMappings();

        public Page GetCachedOrNewPage(string key)
        {
            return CacheCoordinator.GetCachedOrNewPage(key);
        }

        public Page GetCachedPage(string key)
        {
            //TODO: return CacheCoordinator.GetCachedPage(key);
            return null;
        }

        public void AddPage(string key, PageMapContainer container, bool isInitialized = false)
        {
            CacheCoordinator.AddPageToCache(key, container, isInitialized);
        }
    }
}
