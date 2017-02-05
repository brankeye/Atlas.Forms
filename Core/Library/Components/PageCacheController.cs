using System;
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
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageCacheController : IPageCacheController
    {
        protected ICacheController CacheController { get; set; }

        protected IPageFactory PageFactory { get; set; }

        public PageCacheController(ICacheController cacheController, IPageFactory pageFactory)
        {
            CacheController = cacheController;
            PageFactory = pageFactory;
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

        public virtual object GetNewPage(NavigationInfo pageInfo)
        {
            var page = PageFactory.GetNewPage(pageInfo.Page);
            if (pageInfo.HasWrapperPage)
            {
                return PageFactory.GetNewPage(pageInfo.WrapperPage, page);
            }
            return page;
        }

        public virtual object GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters)
        {
            if (pageInfo.NewInstanceRequested)
            {
                return GetNewPage(pageInfo);
            }

            Page pageInstance;
            if (pageInfo.HasWrapperPage)
            {
                var innerPageInstance = TryGetCachedPage(pageInfo.Page, parameters) as Page;
                if (innerPageInstance == null)
                {
                    innerPageInstance = PageFactory.GetNewPage(pageInfo.Page) as Page;
                    PageActionInvoker.InvokeInitialize(innerPageInstance, parameters);
                    AddCachedPagesWithOption(pageInfo.Page, CacheOption.IsCreated);
                }
                else
                {
                    if (innerPageInstance.Parent is NavigationPage)
                    {
                        return innerPageInstance.Parent;
                    }
                }
                pageInstance = PageFactory.GetNewPage(pageInfo.WrapperPage, innerPageInstance) as Page;
                if (pageInstance is NavigationPage)
                {
                    pageInstance.Title = innerPageInstance.Title;
                    pageInstance.Icon = innerPageInstance.Icon;
                }
                IList<PageMapContainer> mapContainers;
                PageCacheMap.Current.Mappings.TryGetValue(pageInfo.Page, out mapContainers);
                if (mapContainers != null)
                {
                    var container = mapContainers.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated && x.Key == pageInfo.Page);
                    var result = CacheController.TryAddPage(pageInfo.Page, new PageCacheContainer(innerPageInstance, container) { Initialized = true });
                }
            }
            else
            {
                pageInstance = TryGetCachedPage(pageInfo.Page, parameters) as Page;
                if (pageInstance == null)
                {
                    pageInstance = PageFactory.GetNewPage(pageInfo.Page) as Page;
                    PageActionInvoker.InvokeInitialize(pageInstance, parameters);
                    AddCachedPagesWithOption(pageInfo.Page, CacheOption.IsCreated);
                }
                IList<PageMapContainer> mapContainers;
                PageCacheMap.Current.Mappings.TryGetValue(pageInfo.Page, out mapContainers);
                if (mapContainers != null)
                {
                    var container = mapContainers.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated && x.Key == pageInfo.Page);
                    var result = CacheController.TryAddPage(pageInfo.Page, new PageCacheContainer(pageInstance, container) { Initialized = true });
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

        public virtual bool TryAddCachedPage(NavigationInfo pageInfo, CacheState cacheState)
        {
            var pageInstance = GetNewPage(pageInfo) as Page;
            var pageMapContainer = new PageMapContainer(cacheState, CacheOption.None, new PageContainer(pageInfo.Page, pageInstance.GetType()));
            var pageCacheContainer = new PageCacheContainer(pageInstance, pageMapContainer);
            return CacheController.TryAddPage(pageInfo.Page, pageCacheContainer);
        }
    }
}
