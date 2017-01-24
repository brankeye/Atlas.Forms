using System.Collections.Generic;
using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class PageCacheService : IPageCacheService
    {
        public static IPageCacheService Current { get; internal set; }

        protected IPageCacheCoordinator CacheCoordinator { get; }

        public PageCacheService(IPageCacheCoordinator cacheCoordinator)
        {
            CacheCoordinator = cacheCoordinator;
        }

        public IReadOnlyDictionary<string, PageCacheContainer> CachedPages => PageCacheStore.Current.GetPageCache();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap => PageCacheMap.Current.GetMappings();

        public virtual Page GetCachedOrNewPage(string key, IParametersService parameters = null)
        {
            return CacheCoordinator.GetCachedOrNewPage(key, parameters ?? new ParametersService());
        }

        public virtual Page GetCachedPage(string key, IParametersService parameters = null)
        {
            CacheCoordinator.GetCachedPage(key, parameters ?? new ParametersService());
            return null;
        }

        public virtual void AddPage(string key, PageMapContainer container, bool isInitialized = false)
        {
            CacheCoordinator.AddPageToCache(key, container, isInitialized);
        }
    }
}
