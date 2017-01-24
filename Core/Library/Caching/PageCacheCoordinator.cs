using System;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Caching
{
    public class PageCacheCoordinator : IPageCacheCoordinator
    {
        public virtual Page GetCachedOrNewPage(string key, IParametersService parameters = null)
        {
            Page nextPage;
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                var innerPageKey = queue.Dequeue();
                var outerPageType = GetPageNavigationStore().PageTypes[outerPageKey];
                var innerPage = GetCachedOrNewPageInternal(innerPageKey, parameters);
                nextPage = Activator.CreateInstance(outerPageType, innerPage) as Page;
                PageProcessor.Process(nextPage);
            }
            else
            {
                nextPage = GetCachedOrNewPageInternal(key, parameters);
            }
            return nextPage;
        }

        public virtual Page GetCachedPage(string key, IParametersService parameters = null)
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
            return null;
        }

        protected virtual Page GetCachedOrNewPageInternal(string key, IParametersService parameters = null)
        {
            var cachedPage = GetCachedPage(key, parameters);
            if (cachedPage != null)
            {
                return cachedPage;
            }

            var pageType = GetPageNavigationStore().PageTypes[key];
            var nextPage = Activator.CreateInstance(pageType) as Page;
            PageActionInvoker.InvokeInitialize(nextPage, parameters);
            var pageMapList = GetPageCacheMap().Mappings[key];
            var mapContainer = pageMapList.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated);
            if (mapContainer != null)
            {
                AddPageToCache(key, nextPage, mapContainer, true);
            }
            PageProcessor.Process(nextPage);
            return nextPage;
        }

        protected virtual PageCacheContainer GetCachedPageInternal(string key)
        {
            var container = GetPageCacheStore().PageCache[key];
            if (container != null)
            {
                if (container.CacheState == CacheState.Default)
                {
                    GetPageCacheStore().PageCache.Remove(container.Key);
                }
                return container;
            }
            return null;
        }

        public virtual void RemoveCachedPages(string key)
        {
            var containers = PageCacheMap.Current.Mappings[key];
            foreach (var container in containers)
            {
                if (container.CacheState == CacheState.Default ||
                    container.CacheState == CacheState.KeepAlive)
                {
                    GetPageCacheStore().PageCache.Remove(container.Key);
                }
            }
        }

        public virtual void LoadCachedPages(string key, CacheOption option = CacheOption.None)
        {
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                key = queue.Dequeue();
            }
            var containers = GetPageCacheMap().Mappings[key];
            if (option != CacheOption.None)
            {
                containers = containers.ToList().Where(x => x.CacheOption == option).ToList();
            }
            foreach (var container in containers)
            {
                if (GetPageCacheStore().PageCache[container.Key] == null)
                {
                    AddPageToCache(container.Key, container);
                }
            }
        }

        public virtual void AddPageToCache(string key, PageMapContainer mapContainer, bool isInitialized = false)
        {
            var pageType = GetPageNavigationStore().PageTypes[mapContainer.Key];
            var page = Activator.CreateInstance(pageType) as Page;
            AddPageToCache(key, page, mapContainer, isInitialized);
        }

        public virtual void AddPageToCache(string key, Page pageInstance, PageMapContainer mapContainer, bool isInitialized = false)
        {
            if (GetPageCacheStore().PageCache[mapContainer.Key] == null)
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
                GetPageCacheStore().PageCache[key] = container;
                PageActionInvoker.InvokeOnPageCached(pageInstance);
            }
        }

        protected virtual PageCacheStore GetPageCacheStore()
        {
            return PageCacheStore.Current;
        }

        protected virtual PageCacheMap GetPageCacheMap()
        {
            return PageCacheMap.Current;
        }

        protected virtual PageNavigationStore GetPageNavigationStore()
        {
            return PageNavigationStore.Current;
        }
    }
}
