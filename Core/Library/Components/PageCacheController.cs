﻿using System;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageCacheController : IPageCacheController
    {
        protected ICacheController CacheController { get; set; }

        protected IPageFactory PageFactory { get; set; }

        public PageCacheController(ICacheController cacheController, INavigationController navigationController)
        {
            CacheController = cacheController;
            PageFactory = CreatePageFactoryInternal(navigationController, this);
        }

        public virtual void AddCachedPages(string key)
        {
            var pageMapContainers = GetMapContainers(key);
            IList<PageCacheContainer> pageCacheList = new List<PageCacheContainer>();
            foreach (var mapContainers in pageMapContainers)
            {
                PageCacheContainer pageContainer;
                PageCacheStore.Current.PageCache.TryGetValue(mapContainers.Key, out pageContainer);
                if (pageContainer == null)
                {
                    if (mapContainers.Key == key && mapContainers.CacheOption == CacheOption.IsCreated)
                    {
                        continue;
                    }
                    var page = PageFactory.GetNewPage(mapContainers.Key) as Page;
                    pageCacheList.Add(new PageCacheContainer(page, new PageMapContainer(mapContainers)));
                }
            }
            CacheController.AddCachedPages(pageCacheList);
        }

        public virtual void AddCachedPagesWithOption(string key, CacheOption cacheOption)
        {
            var pageMapContainers = GetMapContainersWithOption(key, cacheOption);
            IList<PageCacheContainer> pageCacheList = new List<PageCacheContainer>();
            foreach (var mapContainers in pageMapContainers)
            {
                if (mapContainers.Key == key && mapContainers.CacheOption == CacheOption.IsCreated)
                {
                    continue;
                }
                PageCacheContainer pageContainer;
                PageCacheStore.Current.PageCache.TryGetValue(mapContainers.Key, out pageContainer);
                if (pageContainer == null)
                {
                    var page = PageFactory.GetNewPage(mapContainers.Key) as Page;
                    pageCacheList.Add(new PageCacheContainer(page, new PageMapContainer(mapContainers)));
                }
            }
            CacheController.AddCachedPages(pageCacheList);
        }

        public virtual object TryGetCachedPage(string key, IParametersService parameters)
        {
            var page = CacheController.TryGetCachedPage(key, parameters) as Page;
            return page;
        }

        public virtual object GetNewPage(string key)
        {
            return PageFactory.GetNewPage(key);
        }

        public virtual object GetCachedOrNewPage(string key, IParametersService parameters)
        {
            Page pageInstance;
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                var innerPageKey = queue.Dequeue();
                Type outerPageType;
                PageNavigationStore.Current.PageTypes.TryGetValue(outerPageKey, out outerPageType);
                var innerPageInstance = TryGetCachedPage(innerPageKey, parameters) as Page;
                if (innerPageInstance == null)
                {
                    innerPageInstance = PageFactory.GetNewPage(innerPageKey) as Page;
                    PageActionInvoker.InvokeInitialize(innerPageInstance, parameters);
                    AddCachedPagesWithOption(innerPageKey, CacheOption.IsCreated);
                }
                pageInstance = Activator.CreateInstance(outerPageType, innerPageInstance) as Page;
                if (pageInstance is NavigationPage)
                {
                    pageInstance.Title = innerPageInstance.Title;
                    pageInstance.Icon = innerPageInstance.Icon;
                }
                IList<PageMapContainer> mapContainers;
                PageCacheMap.Current.Mappings.TryGetValue(innerPageKey, out mapContainers);
                if (mapContainers != null)
                {
                    var container = mapContainers.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated && x.Key == innerPageKey);
                    var result = CacheController.TryAddPage(innerPageKey, new PageCacheContainer(innerPageInstance, container) { Initialized = true });
                }
            }
            else
            {
                pageInstance = TryGetCachedPage(key, parameters) as Page;
                if (pageInstance == null)
                {
                    pageInstance = PageFactory.GetNewPage(key) as Page;
                    PageActionInvoker.InvokeInitialize(pageInstance, parameters);
                    AddCachedPagesWithOption(key, CacheOption.IsCreated);
                }
                IList<PageMapContainer> mapContainers;
                PageCacheMap.Current.Mappings.TryGetValue(key, out mapContainers);
                if (mapContainers != null)
                {
                    var container = mapContainers.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated && x.Key == key);
                    var result = CacheController.TryAddPage(key, new PageCacheContainer(pageInstance, container) { Initialized = true });
                }
            }

            return pageInstance;
        }

        public virtual IList<PageMapContainer> GetMapContainers(string key)
        {
            return GetAllMapContainers(key);
        }

        protected virtual IList<PageMapContainer> GetAllMapContainers(string key)
        {
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageKey = queue.Dequeue();
                key = queue.Dequeue();
            }
            IList<PageMapContainer> containers;
            PageCacheMap.Current.Mappings.TryGetValue(key, out containers);
            if (containers == null)
            {
                return new List<PageMapContainer>();
            }
            return containers;
        }

        public virtual IList<PageMapContainer> GetMapContainersWithOption(string key, CacheOption cacheOption)
        {
            var containers = GetAllMapContainers(key).ToList().Where(x => x.CacheOption == cacheOption).ToList();
            return containers;
        }

        public virtual void AddCacheContainers(string key, IList<PageCacheContainer> list)
        {
            CacheController.AddCachedPages(list);
        }

        public virtual void RemoveCachedPages(string key)
        {
            CacheController.RemoveCachedPages(key);
        }

        public virtual bool RemovePageFromCache(string key)
        {
            return CacheController.RemovePageFromCache(key);
        }

        public virtual bool TryAddCachedPage(string key, CacheState cacheState)
        {
            var pageInstance = PageFactory.GetNewPage(key) as Page;
            var pageMapContainer = new PageMapContainer(cacheState, CacheOption.None, new PageContainer(key, pageInstance.GetType()));
            var pageCacheContainer = new PageCacheContainer(pageInstance, pageMapContainer);
            return CacheController.TryAddPage(key, pageCacheContainer);
        }

        private IPageFactory CreatePageFactoryInternal(INavigationController navigationController, IPageCacheController pageCacheController)
        {
            return CreatePageFactory(navigationController, pageCacheController);
        }

        protected virtual IPageFactory CreatePageFactory(INavigationController navigationController, IPageCacheController pageCacheController)
        {
            return new PageFactory(navigationController, pageCacheController);
        }
    }
}