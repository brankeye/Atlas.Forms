using System;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
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
            IPageCacheController pageCacheController,
            IPageFactory pageFactory)
        {
            Page = page;
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public virtual void PresentPage(string page, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageCacheController.GetCachedOrNewPage(page, paramService) as Page;
            NavigationController.TrySetNavigation(nextPage);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            NavigationController.AddPageToNavigationStack(page);
            Page.Detail = nextPage;
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            PageCacheController.AddCachedPagesWithOption(page, CacheOption.Appears);
        }
    }
}
