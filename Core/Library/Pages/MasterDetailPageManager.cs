using System;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class MasterDetailPageManager : IMasterDetailPageManager
    {
        protected MasterDetailPage Page { get; set; }

        protected INavigationProvider NavigationProvider { get; set; }

        protected IPageCacheCoordinator CacheCoordinator { get; set; }

        protected IPageStackController PageStackController { get; }

        protected Action<object> TrySetManagersAction { get; }

        protected Func<string, IParametersService, Page> GetCachedOrNewPageFunc { get; }

        public MasterDetailPageManager(
            MasterDetailPage page, 
            INavigationProvider navigationProvider, 
            IPageCacheCoordinator cacheCoordinator, 
            IPageStackController pageStackController, 
            Action<object> trySetManagersAction,
            Func<string, IParametersService, Page> getCachedOrNewPageFunc)
        {
            Page = page;
            NavigationProvider = navigationProvider;
            CacheCoordinator = cacheCoordinator;
            PageStackController = pageStackController;
            TrySetManagersAction = trySetManagersAction;
            GetCachedOrNewPageFunc = getCachedOrNewPageFunc;
        }

        public virtual void PresentPage(string page, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = GetCachedOrNewPageFunc?.Invoke(page, paramService);
            NavigationProvider.TrySetNavigation(nextPage);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            PageStackController.AddPageToNavigationStack(page);
            Page.Detail = nextPage;
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
        }
    }
}
