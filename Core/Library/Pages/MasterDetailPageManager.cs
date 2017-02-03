﻿using System;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Containers;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class MasterDetailPageManager : IMasterDetailPageManager
    {
        protected MasterDetailPage Page { get; set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageCacheController PageCacheController { get; set; }

        public MasterDetailPageManager(
            MasterDetailPage page,
            INavigationController navigationController,
            IPageCacheController pageCacheController)
        {
            Page = page;
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public virtual void PresentPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageCacheController.GetCachedOrNewPage(pageInfo, paramService) as Page;
            NavigationController.TrySetNavigation(nextPage);
            var lastPage = Page.Detail;
            if (lastPage != null)
            {
                PageActionInvoker.InvokeOnPageDisappearing(lastPage, paramService);
            }
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            Page.Detail = nextPage;
            if (lastPage != null)
            {
                PageActionInvoker.InvokeOnPageDisappeared(lastPage, paramService);
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            PageCacheController.AddCachedPagesWithOption(pageInfo.Page, CacheOption.Appears);
        }
    }
}
