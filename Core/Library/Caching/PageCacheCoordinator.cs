using System;
using System.Linq;
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
                var outerPageType = PageNavigationStore.PageTypes[outerPageKey];
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

            var pageType = PageNavigationStore.PageTypes[key];
            var nextPage = Activator.CreateInstance(pageType) as Page;
            PageActionInvoker.InvokeInitialize(nextPage, parameters);
            var pageMapList = PageCacheMap.Mappings[key];
            var mapContainer = pageMapList.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated);
            if (mapContainer != null)
            {
                AddPageToCache(key, nextPage, mapContainer, true);
            }
            return nextPage;
        }

        protected PageCacheContainer GetCachedPageInternal(string key)
        {
            var container = PageCacheStore.PageCache[key];
            if (container != null)
            {
                if (container.CacheState == CacheState.Default)
                {
                    PageCacheStore.PageCache.Remove(container.Key);
                }
                return container;
            }
            return null;
        }

        public void RemoveCachedPages(string key)
        {
            var containers = PageCacheMap.Mappings[key];
            foreach (var container in containers)
            {
                if (container.CacheState == CacheState.Default ||
                    container.CacheState == CacheState.KeepAlive)
                {
                    PageCacheStore.PageCache.Remove(container.Key);
                }
            }
        }

        public void LoadCachedPages(string key, CacheOption option = CacheOption.None)
        {
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                key = queue.Dequeue();
            }
            var containers = PageCacheMap.Mappings[key];
            if (option != CacheOption.None)
            {
                containers = containers.ToList().Where(x => x.CacheOption == option).ToList();
            }
            foreach (var container in containers)
            {
                if (PageCacheStore.PageCache[container.Key] == null)
                {
                    AddPageToCache(container.Key, container);
                }
            }
        }

        public void AddPageToCache(string key, PageMapContainer mapContainer, bool isInitialized = false)
        {
            var pageType = PageNavigationStore.PageTypes[mapContainer.Key];
            var page = Activator.CreateInstance(pageType) as Page;
            AddPageToCache(key, page, mapContainer, isInitialized);
        }

        public void AddPageToCache(string key, Page pageInstance, PageMapContainer mapContainer, bool isInitialized = false)
        {
            if (PageCacheStore.PageCache[mapContainer.Key] == null)
            {
                PageActionInvoker.InvokeOnPageCaching(pageInstance);
                var container = new PageCacheContainer
                {
                    Key = mapContainer.Key,
                    CacheOption = mapContainer.CacheOption,
                    CacheState = mapContainer.CacheState,
                    Initialized = isInitialized,
                    Page = pageInstance,
                    Type = pageInstance.GetType()
                };
                container.Initialized = isInitialized;
                PageCacheStore.PageCache[key] = container;
                PageActionInvoker.InvokeOnPageCached(pageInstance);
            }
        }
    }
}
