using System;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class Presenter : IPresenter
    {
        protected MasterDetailPage Page { get; set; }

        protected INavigationProvider NavigationProvider { get; set; }

        protected IPageCacheCoordinator CacheCoordinator { get; set; }

        protected IPageStackController PageStackController { get; }

        public Presenter(MasterDetailPage page, INavigationProvider navigationProvider, IPageCacheCoordinator cacheCoordinator, IPageStackController pageStackController)
        {
            Page = page;
            NavigationProvider = navigationProvider;
            CacheCoordinator = cacheCoordinator;
            PageStackController = pageStackController;
        }

        public virtual void PresentPage(string page, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page, paramService);
            NavigationProvider.TrySetNavigation(nextPage);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            PageStackController.AddPageToNavigationStack(page);
            Page.Detail = nextPage;
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
        }
    }
}
