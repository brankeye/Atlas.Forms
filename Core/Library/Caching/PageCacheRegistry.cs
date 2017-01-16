using System;
using System.Collections.Generic;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCacheRegistry
    {
        public static void AddPageToCache(string pageKey)
        {
            Page page;
            if (!PageCacheStore.CacheStore.TryGetValue(pageKey, out page))
            {
                var pageType = PageNavigationStore.GetPageType(pageKey);
                page = Activator.CreateInstance(pageType) as Page;
                PageCacheStore.CacheStore.Add(pageKey, page);
            }
        }

        public static void LoadNextPages(string pageKey)
        {
            IList<PageCacheContainer> containers;
            AutoPageCacheStore.CacheStore.TryGetValue(pageKey, out containers);
            if (containers != null)
            {
                foreach (var container in containers)
                {
                    AddPageToCache(container.PageKey);
                }
            }
        }
    }
}
