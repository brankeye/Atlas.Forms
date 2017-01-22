using System;
using System.Collections.Generic;
using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;
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

        public IReadOnlyDictionary<string, PageCacheContainer> CachedPages => Caching.PageCacheStore.GetCacheStore();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap => PageCacheMap.GetCacheStore();

        public Page GetCachedOrNewPage(string key)
        {
            return CacheCoordinator.GetCachedOrNewPage(key);
        }

        public Page GetCachedPage(string key)
        {
            return CacheCoordinator.GetCachedOrNewPage(key);
        }

        public void AddPage(string key)
        {
            //CacheCoordinator.AddPageToCache(key);
        }

        public void AddPage(string key, PageMapContainer container)
        {
            CacheCoordinator.AddPageToCache(key, container);
        }
    }
}
