using System;
using System.Collections.Generic;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;

namespace atlas.core.Library.Caching
{
    public class AutoPageCacheRegistry : IPageCacheRegistry
    {
        public void RegisterPageForCache(string pageKey, string cachedPageKey)
        {
            IList<PageCacheContainer> set;
            if (!AutoPageCacheStore.CacheStore.TryGetValue(pageKey, out set))
            {
                set = new List<PageCacheContainer>();
                AutoPageCacheStore.CacheStore.Add(pageKey, set);
            }
            set.Add(new PageCacheContainer(cachedPageKey));
        }

        public void RegisterPageForCache<TPage>(string cachedPageKey)
        {
            var pageType = typeof(TPage);
            RegisterPageForCache(pageType.Name, cachedPageKey);
        }

        public void RegisterPageForCache<TPage, TCachedPage>()
        {
            var pageType = typeof(TPage);
            var cachePageType = typeof(TCachedPage);
            RegisterPageForCache(pageType.Name, cachePageType.Name);
        }
    }
}
