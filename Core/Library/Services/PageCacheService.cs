using System;
using System.Collections.Generic;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
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

        public virtual IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap => PageCacheMap.Current.GetMappings();

        public virtual Page GetNewPage(string key, IParametersService parameters = null)
        {
            var pageInstance = PageCacheController.GetNewPage(key) as Page;
            PageActionInvoker.InvokeInitialize(pageInstance, parameters ?? new ParametersService());
            return pageInstance;
        }

        public virtual Page GetCachedOrNewPage(string key, IParametersService parameters = null)
        {
            return PageCacheController.GetCachedOrNewPage(key, parameters ?? new ParametersService()) as Page;
        }

        public virtual Page TryGetCachedPage(string key, IParametersService parameters = null)
        {
            return PageCacheController.TryGetCachedPage(key, parameters ?? new ParametersService()) as Page;
        }

        public virtual bool TryAddPage(string key)
        {
            return PageCacheController.TryAddCachedPage(key, CacheState.Default);
        }

        public virtual bool TryAddPageAsKeepAlive(string key)
        {
            return PageCacheController.TryAddCachedPage(key, CacheState.KeepAlive);
        }

        public virtual bool TryAddPageAsSingleInstance(string key)
        {
            return PageCacheController.TryAddCachedPage(key, CacheState.SingleInstance);
        }

        public bool RemovePageFromCache(string key)
        {
            return PageCacheController.RemovePageFromCache(key);
        }
    }
}
