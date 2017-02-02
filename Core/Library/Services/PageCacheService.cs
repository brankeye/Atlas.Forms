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

        public virtual bool TryAddPageAsLifetimeInstance(string key)
        {
            return PageCacheController.TryAddCachedPage(key, CacheState.LifetimeInstance);
        }

        public bool RemovePage(string key)
        {
            return PageCacheController.RemovePageFromCache(key);
        }

        public Page GetCachedOrNewPage<TClass>(IParametersService parameters = null)
        {
            return GetCachedOrNewPage(typeof(TClass).Name, parameters);
        }

        public Page GetNewPage<TClass>(IParametersService parameters = null)
        {
            return GetNewPage(typeof(TClass).Name, parameters);
        }

        public Page TryGetCachedPage<TClass>(IParametersService parameters = null)
        {
            return TryGetCachedPage(typeof(TClass).Name, parameters);
        }

        public bool TryAddPage(string key, Page page)
        {
            return false;
        }

        public bool TryAddPage<TClass>(Page page)
        {
            return TryAddPage(typeof(TClass).Name, page);
        }

        public bool TryAddPage<TClass>()
        {
            return TryAddPage(typeof(TClass).Name);
        }

        public bool TryAddPageAsKeepAlive<TClass>()
        {
            return TryAddPageAsKeepAlive(typeof(TClass).Name);
        }

        public bool TryAddPageAsSingleInstance<TClass>()
        {
            return TryAddPageAsSingleInstance(typeof(TClass).Name);
        }

        public bool TryAddPageAsLifetimeInstance<TClass>()
        {
            return TryAddPageAsLifetimeInstance(typeof(TClass).Name);
        }

        public bool RemovePage<TClass>()
        {
            return RemovePage(typeof(TClass).Name);
        }
    }
}
