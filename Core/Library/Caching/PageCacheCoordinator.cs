using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                Type outerPageType;
                GetPageNavigationStore().PageTypes.TryGetValue(outerPageKey, out outerPageType);
                var innerPage = GetCachedOrNewPageInternal(innerPageKey, parameters);
                nextPage = Activator.CreateInstance(outerPageType, innerPage) as Page;
                if (nextPage is NavigationPage)
                {
                    nextPage.Title = innerPage.Title;
                    nextPage.Icon = innerPage.Icon;
                }
                PageProcessor.Process(nextPage);
            }
            else
            {
                nextPage = GetCachedOrNewPageInternal(key, parameters);
            }
            return nextPage;
        }

        public virtual Page GetNewPage(string key)
        {
            Type pageType;
            GetPageNavigationStore().PageTypes.TryGetValue(key, out pageType);
            var nextPage = Activator.CreateInstance(pageType) as Page;
            IList<PageMapContainer> pageMapList;
            GetPageCacheMap().Mappings.TryGetValue(key, out pageMapList);
            var mapContainer = pageMapList?.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated);
            if (mapContainer != null)
            {
                AddPageToCache(key, nextPage, mapContainer, true);
            }
            PageProcessor.Process(nextPage);
            return nextPage;
        }

        public virtual PageCacheContainer TryGetCachedPage(string key)
        {
            PageCacheContainer container;
            GetPageCacheStore().PageCache.TryGetValue(key, out container);
            if (container?.CacheState == CacheState.Default)
            {
                GetPageCacheStore().PageCache.Remove(container.Key);
            }
            return container;
        }

        protected virtual Page GetCachedOrNewPageInternal(string key, IParametersService parameters = null)
        {
            var cachedPage = TryGetCachedPage(key);
            if (cachedPage != null)
            {
                return cachedPage.Page;
            }

            Type pageType;
            GetPageNavigationStore().PageTypes.TryGetValue(key, out pageType);
            if (pageType == null)
            {
                return null;
            }
            var nextPage = Activator.CreateInstance(pageType) as Page;
            PageActionInvoker.InvokeInitialize(nextPage, parameters);
            IList<PageMapContainer> pageMapList;
            GetPageCacheMap().Mappings.TryGetValue(key, out pageMapList);
            var mapContainer = pageMapList?.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated);
            if (mapContainer != null)
            {
                AddPageToCache(key, nextPage, mapContainer, true);
            }
            PageProcessor.Process(nextPage);
            return nextPage;
        }

        protected virtual PageCacheContainer TryGetCachedPageInternal(string key)
        {
            PageCacheContainer container;
            GetPageCacheStore().PageCache.TryGetValue(key, out container);
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
            IList<PageMapContainer> containers; 
            GetPageCacheMap().Mappings.TryGetValue(key, out containers);
            if (containers == null)
            {
                return;
            }
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

            IList<PageMapContainer> containers;
            GetPageCacheMap().Mappings.TryGetValue(key, out containers);
            if (containers == null)
            {
                return;
            }
            if (option != CacheOption.None)
            {
                containers = containers.ToList().Where(x => x.CacheOption == option).ToList();
            }
            foreach (var container in containers)
            {
                PageCacheContainer pageContainer;
                GetPageCacheStore().PageCache.TryGetValue(container.Key, out pageContainer);
                if (pageContainer == null)
                {
                    AddPageToCache(container.Key, container);
                }
            }
        }

        public virtual void AddPageToCache(string key, PageMapContainer mapContainer, bool isInitialized = false)
        {
            Type pageType;
            GetPageNavigationStore().PageTypes.TryGetValue(key, out pageType);
            if (pageType == null)
            {
                return;
            }
            var page = Activator.CreateInstance(pageType) as Page;
            AddPageToCache(key, page, mapContainer, isInitialized);
        }

        public virtual void AddPageToCache(string key, Page pageInstance, PageMapContainer mapContainer, bool isInitialized = false)
        {
            PageCacheContainer containerCheck;
            GetPageCacheStore().PageCache.TryGetValue(key, out containerCheck);
            if (containerCheck == null)
            {
                PageActionInvoker.InvokeOnPageCaching(pageInstance);
                var container = new PageCacheContainer(pageInstance, mapContainer)
                {
                    Initialized = isInitialized
                };
                GetPageCacheStore().PageCache.Add(key, container);
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

        //public IList<Page> GetSequencePages(string key, IParametersService parameters = null)
        //{
        //    IList<Page> list = new List<Page>();
        //    if (PageKeyParser.IsSequence(key))
        //    {
        //        var queue = PageKeyParser.GetPageKeysFromSequence(key);
        //        while (queue.Count > 0)
        //        {
        //            var pageKey = queue.Dequeue();
        //            Type pageType;
        //            GetPageNavigationStore().PageTypes.TryGetValue(pageKey, out pageType);
        //            var constructorTakesPage =
        //                pageType.GetTypeInfo().DeclaredConstructors
        //                .FirstOrDefault(x => x.GetParameters()
        //                .FirstOrDefault(y => y.ParameterType == typeof(Page)) != null)
        //                != null;
        //            if (constructorTakesPage)
        //            {
        //                var nextPageKey = queue.Dequeue();
        //                Type nextPageType;
        //                GetPageNavigationStore().PageTypes.TryGetValue(nextPageKey, out nextPageType);
        //                var innerPage = GetCachedOrNewPageInternal(nextPageKey, parameters);
        //                var nextPage = Activator.CreateInstance(pageType, innerPage) as Page;
        //                list.Add(nextPage);
        //                list.Add(innerPage);
        //            }
        //            else if (pageType.GetTypeInfo().IsSubclassOf(typeof(TabbedPage)))
        //            {
        //                if (queue.Count > 0)
        //                {
        //                    var nextPage = GetCachedOrNewPageInternal(pageKey, parameters);
        //                    list.Add(nextPage);
        //                }
        //                else
        //                {
        //                    var nextPage = GetCachedOrNewPageInternal(pageKey, parameters);
        //                    list.Add(nextPage);
        //                }
        //            }
        //            else
        //            {
        //                var nextPage = GetCachedOrNewPageInternal(pageKey, parameters);
        //                list.Add(nextPage);
        //            }
        //        }

        //        for (var i = 0; i < list.Count; i++)
        //        {
        //            var page = list[i];
        //            if (page is MasterDetailPage && i < list.Count - 1)
        //            {
        //                var masterDetailPage = page as MasterDetailPage;
        //                masterDetailPage.Detail = list[i + 1];
        //            }
        //            else if (page is TabbedPage)
        //            {

        //            }
        //        }
        //    }
        //    else
        //    {
        //        var nextPage = GetCachedOrNewPageInternal(key, parameters);
        //        list.Add(nextPage);
        //    }
        //    return list;
        //}
    }
}
