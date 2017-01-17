using System;
using atlas.core.Library.Navigation;
using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCache
    {
        public static void AddPage(string key)
        {
            if (PageCacheStore.TryGetPage(key) == null)
            {
                var pageType = PageNavigationStore.GetPageType(key);
                var page = Activator.CreateInstance(pageType) as Page;
                PageMethodInvoker.InvokeOnPageCaching(page);
                PageCacheStore.AddPage(key, page);
                PageMethodInvoker.InvokeOnPageCached(page);
            }
        }

        public static bool RemovePage(string key)
        {
            return PageCacheStore.RemovePage(key);
        }

        public static void PreloadCachedPages(string key)
        {
            var containers = PageCacheMap.GetCachedPages(key);
            foreach (var container in containers)
            {
                AddPage(container.Key);
            }
        }
    }
}
