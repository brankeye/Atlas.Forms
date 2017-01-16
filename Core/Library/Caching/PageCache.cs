using System;
using System.Collections.Generic;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCache
    {
        public static void AddPageToCache(string key)
        {
            if (PageCacheStore.TryGetPage(key) == null)
            {
                var pageType = PageNavigationStore.GetPageType(key);
                var page = Activator.CreateInstance(pageType) as Page;
                PageCacheStore.AddPage(key, page);
            }
        }

        public static void PreloadCachedPages(string key)
        {
            var containers = PageCacheMap.GetCachedPages(key);
            foreach (var container in containers)
            {
                AddPageToCache(container.Key);
            }
        }
    }
}
