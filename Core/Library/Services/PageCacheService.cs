using System;
using System.Collections.Generic;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class PageCacheService : IPageCacheService
    {
        public static IPageCacheService Current { get; internal set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageCacheController PageCacheController { get; }

        public PageCacheService(INavigationController navigationController, IPageCacheController pageCacheController)
        {
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public virtual IReadOnlyDictionary<string, PageCacheContainer> CachedPages => PageCacheStore.Current.GetPageCache();

        public virtual Page GetNewPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var pageInstance = PageCacheController.GetNewPage(pageInfo) as Page;
            PageActionInvoker.InvokeInitialize(pageInstance, parameters ?? new ParametersService());
            return pageInstance;
        }

        public virtual Page GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            return PageCacheController.GetCachedOrNewPage(pageInfo, parameters ?? new ParametersService()) as Page;
        }

        public virtual Page TryGetCachedPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            return PageCacheController.TryGetCachedPage(pageInfo.Page, parameters ?? new ParametersService()) as Page;
        }

        public virtual bool TryAddPage(NavigationInfo pageInfo)
        {
            return PageCacheController.TryAddCachedPage(pageInfo, CacheState.Default);
        }

        public virtual bool TryAddPageAsKeepAlive(NavigationInfo pageInfo)
        {
            return PageCacheController.TryAddCachedPage(pageInfo, CacheState.KeepAlive);
        }

        public virtual bool TryAddPageAsSingleInstance(NavigationInfo pageInfo)
        {
            return PageCacheController.TryAddCachedPage(pageInfo, CacheState.SingleInstance);
        }

        public virtual bool TryAddPageAsLifetimeInstance(NavigationInfo pageInfo)
        {
            return PageCacheController.TryAddCachedPage(pageInfo, CacheState.LifetimeInstance);
        }

        public bool RemovePage(NavigationInfo pageInfo)
        {
            return PageCacheController.RemovePageFromCache(pageInfo.Page);
        }
    }
}
