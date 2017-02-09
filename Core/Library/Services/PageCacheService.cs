﻿using System;
using System.Collections.Generic;
using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Interfaces.Utilities;
using Atlas.Forms.Navigation;
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

        public PageCacheService(IPageRetriever pageRetriever, ICacheController cacheController)
        {
            PageRetriever = pageRetriever;
            CacheController = cacheController;
        }

        public virtual IReadOnlyDictionary<string, CacheInfo> CachedPages => CacheController.GetPageCache();

        public virtual Page GetNewPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var pageInstance = PageRetriever.GetNewPage(pageInfo);
            PageActionInvoker.InvokeInitialize(pageInstance, parameters ?? new ParametersService());
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

        public virtual bool TryAddPage(NavigationInfo pageInfo)
        {
            var pageInstance = PageRetriever.GetNewPage(pageInfo);
            var cacheInfo = new CacheInfo(pageInstance, false, new MapInfo
            {
                Key = pageInfo.Page,
                Type = pageInstance.GetType(),
                CacheState = CacheState.Default,
                CacheOption = CacheOption.None
            });
            return CacheController.TryAddCacheInfo(pageInfo.Page, cacheInfo);
        }

        public bool TryAddExistingPage(NavigationInfo pageInfo, Page page)
        {
            var cacheInfo = new CacheInfo(page, false, new MapInfo
            {
                Key = pageInfo.Page,
                Type = page.GetType(),
                CacheState = CacheState.Default,
                CacheOption = CacheOption.None
            });
            return CacheController.TryAddCacheInfo(pageInfo.Page, cacheInfo);
        }

        public virtual bool RemovePage(NavigationInfo pageInfo)
        {
            return CacheController.RemoveCacheInfo(pageInfo.Page);
        }
    }
}
