using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Info;
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
            var pageMapContainers = GetMapContainers(key).Where(x => x.Key != key);
            var pageCacheList = AddCachedPages(pageMapContainers.ToList());
            CacheController.AddCachedPages(pageCacheList);
        }

        public virtual void AddCachedPagesWithOption(string key, CacheOption cacheOption)
        {
            var pageMapContainers = GetMapContainersWithOption(key, cacheOption).Where(x => x.Key != key);
            var pageCacheList = AddCachedPages(pageMapContainers.ToList());
            CacheController.AddCachedPages(pageCacheList);
        }

        protected virtual IList<PageCacheInfo> AddCachedPages(IList<PageMapInfo> list)
        {
            var pageCacheList = new List<PageCacheInfo>();
            foreach (var mapContainers in list)
            {
                PageCacheInfo pageInfo;
                PageCacheStore.Current.PageCache.TryGetValue(mapContainers.Key, out pageInfo);
                if (pageInfo == null)
                {
                    var page = PageFactory.GetNewPage(mapContainers.Key) as Page;
                    pageCacheList.Add(new PageCacheInfo(page, new PageMapInfo(mapContainers)));
                }
            }
            return pageCacheList;
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
            Page pageInstance;
            if (pageInfo.NewInstanceRequested)
            {
                pageInstance = GetNewPage(pageInfo) as Page;
                PageActionInvoker.InvokeInitialize(pageInstance, parameters);
                AddCachedPagesWithOption(pageInfo.Page, CacheOption.IsCreated);
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
                IList<PageMapInfo> mapContainers;
                PageCacheMap.Current.Mappings.TryGetValue(pageInfo.Page, out mapContainers);
                if (mapContainers != null)
                {
                    var container = mapContainers.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated && x.Key == pageInfo.Page);
                    var result = CacheController.TryAddPage(pageInfo.Page, new PageCacheInfo(pageInstance, container) { Initialized = true });
                }
                if (pageInfo.HasWrapperPage)
                {
                    if (pageInstance?.Parent is NavigationPage)
                    {
                        return pageInstance.Parent;
                    }
                    var innerPageInstance = pageInstance;
                    pageInstance = PageFactory.GetNewPage(pageInfo.WrapperPage, innerPageInstance) as Page;
                    if (pageInstance is NavigationPage)
                    {
                        pageInstance.Title = innerPageInstance?.Title;
                        pageInstance.Icon = innerPageInstance?.Icon;
                    }
                }
            }
            return pageInstance;
        }

        public virtual IList<PageMapInfo> GetMapContainers(string key)
        {
            return GetAllMapContainers(key);
        }

        protected virtual IList<PageMapInfo> GetAllMapContainers(string key)
        {
            IList<PageMapInfo> containers;
            PageCacheMap.Current.Mappings.TryGetValue(key, out containers);
            if (containers == null)
            {
                return new List<PageMapInfo>();
            }
            return containers;
        }

        public virtual IList<PageMapInfo> GetMapContainersWithOption(string key, CacheOption cacheOption)
        {
            var containers = GetAllMapContainers(key).ToList().Where(x => x.CacheOption == cacheOption).ToList();
            return containers;
        }

        public virtual void AddCacheContainers(string key, IList<PageCacheInfo> list)
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
            var pageMapContainer = new PageMapInfo(cacheState, CacheOption.None, new PageInfo(pageInfo.Page, pageInstance.GetType()));
            var pageCacheContainer = new PageCacheInfo(pageInstance, pageMapContainer);
            return CacheController.TryAddPage(pageInfo.Page, pageCacheContainer);
        }

        public virtual bool TryAddCachedPage(Page pageInstance, PageMapInfo mapInfo)
        {
            var pageKey = PageKeyStore.Current.GetPageContainer(pageInstance);
            var pageCacheContainer = new PageCacheInfo(pageInstance, mapInfo);
            return CacheController.TryAddPage(pageKey.Key, pageCacheContainer);
        }
    }
}
