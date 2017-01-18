using System;
using atlas.core.Library.Behaviors;
using atlas.core.Library.Enums;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCacheCoordinator : IPageCacheCoordinator
    {
        public Page GetCachedOrNewPage(string key)
        {
            Page nextPage;
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                var innerPageKey = queue.Dequeue();
                var outerPageType = PageNavigationStore.GetPageType(outerPageKey);
                var innerPage = GetCachedOrNewPageInternal(innerPageKey);
                nextPage = Activator.CreateInstance(outerPageType, innerPage) as Page;
                (nextPage as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior());
                LoadCachedPages(innerPageKey);
            }
            else
            {
                nextPage = GetCachedOrNewPageInternal(key);
                LoadCachedPages(key);
            }
            return nextPage;
        }

        protected Page GetCachedOrNewPageInternal(string key)
        {
            var container = PageCacheStore.TryGetPage(key);
            if (container != null)
            {
                if (container.CacheState == CacheState.Default ||
                    container.CacheState == CacheState.KeepAlive)
                {
                    PageCacheStore.RemovePage(container.Key);
                }
                return container.Page;
            }
            var pageType = PageNavigationStore.GetPageType(key);
            return Activator.CreateInstance(pageType) as Page;
        }

        public void RemoveCachedPages(string key)
        {
            var containers = PageCacheMap.GetCachedPages(key);
            foreach (var container in containers)
            {
                if (container.CacheState == CacheState.Default)
                {
                    PageCacheStore.RemovePage(container.Key);
                }
            }
        }

        public void LoadCachedPages(string key)
        {
            var containers = PageCacheMap.GetCachedPages(key);
            foreach (var container in containers)
            {
                AddPageToCache(container);
            }
        }

        public void AddPageToCache(string key)
        {
            if (PageCacheStore.TryGetPage(key) == null)
            {
                var pageType = PageNavigationStore.GetPageType(key);
                var page = Activator.CreateInstance(pageType) as Page;
                PageMethodInvoker.InvokeOnPageCaching(page);
                PageCacheStore.AddPage(key, new PageCacheContainer(key, pageType, page));
                PageMethodInvoker.InvokeOnPageCached(page);
            }
        }

        public void AddPageToCache(PageMapContainer mapContainer)
        {
            if (PageCacheStore.TryGetPage(mapContainer.Key) == null)
            {
                var pageType = PageNavigationStore.GetPageType(mapContainer.Key);
                var page = Activator.CreateInstance(pageType) as Page;
                PageMethodInvoker.InvokeOnPageCaching(page);
                PageCacheStore.AddPage(mapContainer.Key, new PageCacheContainer(mapContainer.Key, pageType, mapContainer.CacheState, page));
                PageMethodInvoker.InvokeOnPageCached(page);
            }
        }
    }
}
