using System;
using System.Collections.Generic;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Interfaces.Utilities;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Infos;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class PageCacheService : IPageCacheService
    {
        public static IPageCacheService Current => Instance.Current;

        protected static ILazySingleton<IPageCacheService> Instance { get; set; }
            = new LazySingleton<IPageCacheService>(() => null);

        public static void SetCurrent(Func<IPageCacheService> func)
        {
            Instance.SetCurrent(func);
        }

        protected IPageRetriever PageRetriever { get; }

        protected ICacheController CacheController { get; }

        protected IPublisher Publisher { get; }

        public PageCacheService(IPageRetriever pageRetriever, ICacheController cacheController, IPublisher publisher)
        {
            PageRetriever = pageRetriever;
            CacheController = cacheController;
            Publisher = publisher;
        }

        public virtual IReadOnlyDictionary<string, CacheInfo> CachedPages => CacheController.GetPageCache();

        public virtual Page GetNewPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var pageInstance = PageRetriever.GetNewPage(pageInfo);
            Publisher.SendPageInitializeMessage(pageInstance, parameters ?? new ParametersService());
            return pageInstance;
        }

        public virtual Page GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            return PageRetriever.GetCachedOrNewPage(pageInfo, parameters ?? new ParametersService());
        }

        public virtual Page TryGetCachedPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            return PageRetriever.TryGetCachedPage(pageInfo.Page, parameters ?? new ParametersService());
        }

        public virtual bool TryAddPage(TargetPageInfo targetPageInfo)
        {
            var navigationInfo = new NavigationInfo(targetPageInfo.Key);
            var pageInstance = PageRetriever.GetNewPage(navigationInfo);
            var cacheInfo = new CacheInfo(pageInstance, true, targetPageInfo);
            return CacheController.TryAddCacheInfo(targetPageInfo.Key, cacheInfo);
        }

        public bool TryAddExistingPage(TargetPageInfo targetPageInfo, Page page)
        {
            var cacheInfo = new CacheInfo(page, true, targetPageInfo);
            return CacheController.TryAddCacheInfo(targetPageInfo.Key, cacheInfo);
        }

        public virtual bool RemovePage(NavigationInfo pageInfo)
        {
            return CacheController.RemoveCacheInfo(pageInfo.Page);
        }
    }
}
