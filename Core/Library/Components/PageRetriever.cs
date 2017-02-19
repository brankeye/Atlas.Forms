using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageRetriever : IPageRetriever
    {
        protected ICacheController CacheController { get; }

        protected IPageFactory PageFactory { get; }

        protected IPublisher Publisher { get; }

        public PageRetriever(ICacheController cacheController, 
                             IPageFactory pageFactory, 
                             IPublisher publisher)
        {
            CacheController = cacheController;
            PageFactory = pageFactory;
            Publisher = publisher;
        }

        public virtual Page TryGetCachedPage(string key, IParametersService parameters)
        {
            var pageCacheInfo = CacheController.TryGetCacheInfo(key);
            if (pageCacheInfo != null)
            {
                if (!pageCacheInfo.Initialized)
                {
                    Publisher.SendPageInitializeMessage(pageCacheInfo.Page, parameters);
                    pageCacheInfo.Initialized = true;
                }
            }
            return pageCacheInfo?.Page;
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
                Publisher.SendPageInitializeMessage(pageInstance, parameters);
            }
            else
            {
                pageInstance = TryGetCachedPage(pageInfo.Page, parameters);
                if (pageInstance == null)
                {
                    pageInstance = PageFactory.GetNewPage(pageInfo.Page);
                    Publisher.SendPageInitializeMessage(pageInstance, parameters);
                }
                if (pageInfo.HasWrapperPage)
                {
                    if (pageInstance?.Parent is NavigationPage)
                    {
                        return (Page) pageInstance.Parent;
                    }
                    var innerPageInstance = pageInstance;
                    pageInstance = PageFactory.GetNewPage(pageInfo.WrapperPage, innerPageInstance);
                    if (pageInstance is NavigationPage)
                    {
                        pageInstance.Title = innerPageInstance?.Title;
                        pageInstance.Icon = innerPageInstance?.Icon;
                    }
                }
            }
            Publisher.SendPageCreatedMessage(pageInstance);
            return pageInstance;
        }
    }
}
