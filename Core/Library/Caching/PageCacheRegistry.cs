using System;
using System.Collections.Generic;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using atlas.core.Library.Pages;

namespace atlas.core.Library.Caching
{
    public class PageCacheRegistry : IPageCacheRegistry
    {
        public void RegisterPageForCache(string pageKey, string cachedPageKey)
        {
            var type = PageNavigationStore.GetPageType(cachedPageKey);
            PageCacheMap.AddPageContainer(pageKey, new PageContainer(cachedPageKey, type));
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
