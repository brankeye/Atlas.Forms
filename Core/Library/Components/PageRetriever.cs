using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Infos;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageRetriever : IPageRetriever
    {
        protected ICacheController CacheController { get; set; }

        protected IPageFactory PageFactory { get; set; }

        public PageRetriever(ICacheController cacheController, IPageFactory pageFactory)
        {
            CacheController = cacheController;
            PageFactory = pageFactory;
        }

        public virtual Page TryGetCachedPage(string key, IParametersService parameters)
        {
            var pageCacheInfo = CacheController.TryGetCacheInfo(key);
            if (pageCacheInfo.CacheState == CacheState.Default)
            {
                PageCacheStore.Current.PageCache.Remove(pageCacheInfo.Key);
            }
            if (!pageCacheInfo.Initialized)
            {
                PageActionInvoker.InvokeInitialize(pageCacheInfo.Page, parameters);
                pageCacheInfo.Initialized = true;
            }
            return pageCacheInfo.Page;
        }

        public virtual Page GetNewPage(NavigationInfo pageInfo)
        {
            var page = PageFactory.GetNewPage(pageInfo.Page);
            if (pageInfo.HasWrapperPage)
            {
                return PageFactory.GetNewPage(pageInfo.WrapperPage, page);
            }
            return page;
        }

        public virtual Page GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters)
        {
            Page pageInstance;
            if (pageInfo.NewInstanceRequested)
            {
                pageInstance = GetNewPage(pageInfo);
                PageActionInvoker.InvokeInitialize(pageInstance, parameters);
                CachePubSubService.Publisher.SendPageCreatedMessage(pageInstance);
            }
            else
            {
                pageInstance = TryGetCachedPage(pageInfo.Page, parameters);
                if (pageInstance == null)
                {
                    pageInstance = PageFactory.GetNewPage(pageInfo.Page);
                    PageActionInvoker.InvokeInitialize(pageInstance, parameters);
                    CachePubSubService.Publisher.SendPageCreatedMessage(pageInstance);
                }
                IList<MapInfo> mapContainers;
                PageCacheMap.Current.Mappings.TryGetValue(pageInfo.Page, out mapContainers);
                if (mapContainers != null)
                {
                    var container = mapContainers.FirstOrDefault(x => x.CacheOption == CacheOption.IsCreated && x.Key == pageInfo.Page);
                    var result = CacheController.TryAddCacheInfo(pageInfo.Page, new CacheInfo(pageInstance, container) { Initialized = true });
                }
                if (pageInfo.HasWrapperPage)
                {
                    if (pageInstance?.Parent is NavigationPage)
                    {
                        return (Page) pageInstance.Parent;
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
    }
}
