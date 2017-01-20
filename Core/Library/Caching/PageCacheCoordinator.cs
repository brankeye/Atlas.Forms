using System;
using System.Threading.Tasks;
using atlas.core.Library.Behaviors;
using atlas.core.Library.Enums;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    internal class PageCacheCoordinator : IPageCacheCoordinator
    {
        public Page GetCachedOrNewPage(string key, IParametersService parameters = null)
        {
            Page nextPage;
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                var innerPageKey = queue.Dequeue();
                var outerPageType = PageNavigationStore.GetPageType(outerPageKey);
                var innerPage = GetCachedOrNewPageInternal(innerPageKey, parameters);
                PageProcessor.Process(innerPage);
                nextPage = Activator.CreateInstance(outerPageType, innerPage) as Page;
                PageProcessor.Process(nextPage);
            }
            else
            {
                nextPage = GetCachedOrNewPageInternal(key, parameters);
            }
            return nextPage;
        }

        protected Page GetCachedOrNewPageInternal(string key, IParametersService parameters = null)
        {
            var pageContainer = GetCachedPageInternal(key);
            if (pageContainer?.Page != null)
            {
                if (!pageContainer.Initialized)
                {
                    PageActionInvoker.InvokeInitialize(pageContainer.Page, parameters);
                    pageContainer.Initialized = true;
                }
                return pageContainer.Page;
            }

            var pageType = PageNavigationStore.GetPageType(key);
            var nextPage = Activator.CreateInstance(pageType) as Page;
            PageActionInvoker.InvokeInitialize(nextPage, parameters);
            return nextPage;
        }

        protected PageCacheContainer GetCachedPageInternal(string key)
        {
            var container = PageCacheStore.TryGetPage(key);
            if (container != null)
            {
                if (container.CacheState == CacheState.Default)
                {
                    PageCacheStore.RemovePage(container.Key);
                }
                return container;
            }
            return null;
        }

        public Task RemoveCachedPages(string key)
        {
            return Task.Run(() =>
            {
                var containers = PageCacheMap.GetCachedPages(key);
                foreach (var container in containers)
                {
                    if (container.CacheState == CacheState.Default ||
                        container.CacheState == CacheState.KeepAlive)
                    {
                        PageCacheStore.RemovePage(container.Key);
                    }
                }
            });
        }

        public Task LoadCachedPages(string key)
        {
            return Task.Run(() =>
            {
                if (PageKeyParser.IsSequence(key))
                {
                    var queue = PageKeyParser.GetPageKeysFromSequence(key);
                    var outerPageKey = queue.Dequeue();
                    key = queue.Dequeue();
                }
                var containers = PageCacheMap.GetCachedPages(key);
                foreach (var container in containers)
                {
                    AddPageToCache(container.Key, container);
                }
            });
        }

        public void AddPageToCache(string key)
        {
            if (PageCacheStore.TryGetPage(key) == null)
            {
                var pageType = PageNavigationStore.GetPageType(key);
                var page = Activator.CreateInstance(pageType) as Page;
                PageActionInvoker.InvokeOnPageCaching(page);
                PageCacheStore.AddPage(key, new PageCacheContainer(key, pageType, page));
                PageActionInvoker.InvokeOnPageCached(page);
            }
        }

        public void AddPageToCache(string key, PageMapContainer mapContainer)
        {
            if (PageCacheStore.TryGetPage(mapContainer.Key) == null)
            {
                var pageType = PageNavigationStore.GetPageType(mapContainer.Key);
                var page = Activator.CreateInstance(pageType) as Page;
                PageActionInvoker.InvokeOnPageCaching(page);
                PageCacheStore.AddPage(mapContainer.Key, new PageCacheContainer(key, pageType, mapContainer.CacheState, page));
                PageActionInvoker.InvokeOnPageCached(page);
            }
        }
    }
}
