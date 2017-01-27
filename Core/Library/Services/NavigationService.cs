using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class NavigationService : INavigationService
    {
        public static INavigationService Current { get; internal set; }

        public IReadOnlyList<IPageContainer> NavigationStack => PageStackController.NavigationStack.ToList();

        public IReadOnlyList<IPageContainer> ModalStack => PageStackController.ModalStack.ToList();

        public INavigationProvider NavigationProvider { get; set; }

        protected IApplicationProvider ApplicationProvider { get; }

        protected IPageCacheCoordinator CacheCoordinator { get; }

        protected IPageStackController PageStackController { get; }

        public NavigationService(IApplicationProvider applicationProvider, INavigationProvider navigationProvider, IPageCacheCoordinator cacheCoordinator, IPageStackController pageStackController)
        {
            ApplicationProvider = applicationProvider;
            NavigationProvider = navigationProvider;
            CacheCoordinator = cacheCoordinator;
            PageStackController = pageStackController;
        }

        public virtual void InsertPageBefore(string page, string before, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = GetCachedOrNewPage(page, paramService);
            TrySetManagers(nextPage);
            var navigationStack = PageStackController.NavigationStack;
            var beforeIndex = navigationStack.IndexOf(navigationStack.FirstOrDefault(x => x.Key == before));
            var beforePage = NavigationProvider.Navigation.NavigationStack.ElementAtOrDefault(beforeIndex);
            NavigationProvider.Navigation.InsertPageBefore(nextPage, beforePage);
            navigationStack.Insert(beforeIndex, new PageContainer(page, nextPage.GetType()));
        }

        public virtual async Task<IPageContainer> PopAsync(IParametersService parameters = null)
        {
            return await PopAsync(true, parameters);
        }

        public virtual async Task<IPageContainer> PopAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(animated, parameters);
        }

        public virtual async Task<IPageContainer> PopModalAsync(IParametersService parameters = null)
        {
            return await PopModalAsync(true, parameters);
        }

        public virtual async Task<IPageContainer> PopModalAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(animated, parameters, true);
        }

        protected virtual async Task<IPageContainer> PopInternalAsync(bool animated, IParametersService parameters = null, bool useModal = false)
        {
            var paramService = parameters ?? new ParametersService();
            var pageStack = useModal ? PageStackController.ModalStack
                                     : PageStackController.NavigationStack;
            CacheCoordinator.RemoveCachedPages(pageStack[pageStack.Count - 1].Key);
            var currentPage = pageStack[pageStack.Count - 1];
            PageActionInvoker.InvokeOnPageDisappearing(currentPage, paramService);
            if (useModal)
            {
                await NavigationProvider.Navigation.PopModalAsync(animated);
            }
            else
            {
                await NavigationProvider.Navigation.PopAsync(animated);
            }
            var pageContainer = pageStack.Last();
            pageStack.Remove(pageContainer);
            PageActionInvoker.InvokeOnPageDisappeared(currentPage, paramService);
            CacheCoordinator.LoadCachedPages(pageStack.Last().Key, CacheOption.Appears);
            return pageContainer;
        }

        public virtual async Task PopToRootAsync()
        {
            await PopToRootAsync(true);
        }

        public virtual async Task PopToRootAsync(bool animated)
        {
            while (NavigationProvider.Navigation.NavigationStack.Count > 1)
            {
                await PopAsync(animated);
            }
        }

        public virtual async Task PushAsync(string page, IParametersService parameters = null)
        {
            await PushAsync(page, true, parameters);
        }

        public virtual async Task PushAsync(string page, bool animated, IParametersService parameters = null)
        {
            await PushInternalAsync(page, animated, parameters);
        }

        public virtual async Task PushModalAsync(string page, IParametersService parameters = null)
        {
            await PushModalAsync(page, true, parameters);
        }

        public virtual async Task PushModalAsync(string page, bool animated, IParametersService parameters = null)
        {
            await PushInternalAsync(page, animated, parameters, true);
        }

        protected virtual async Task PushInternalAsync(string page, bool animated, IParametersService parameters = null, bool useModal = false)
        {
            var paramService = parameters ?? new ParametersService();
            var pageStack = useModal ? PageStackController.ModalStack
                                     : PageStackController.NavigationStack;
            if (pageStack.Count > 0)
            {
                CacheCoordinator.RemoveCachedPages(pageStack.Last().Key);
            }
            var nextPage = GetCachedOrNewPage(page, paramService);
            TrySetManagers(nextPage);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            if (useModal)
            {
                await NavigationProvider.Navigation.PushModalAsync(nextPage, animated);
                PageStackController.AddPageToModalStack(page);
            }
            else
            {
                await NavigationProvider.Navigation.PushAsync(nextPage, animated);
                PageStackController.AddPageToNavigationStack(page);
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
        }

        public void RemovePage(string page)
        {
            var navigationStack = PageStackController.NavigationStack;
            for (var i = navigationStack.Count - 1; i >= 0; i--)
            {
                if (navigationStack[i].Key == page)
                {
                    NavigationProvider.Navigation.RemovePage(NavigationProvider.Navigation.NavigationStack[i]);
                    navigationStack.RemoveAt(i);
                    break;
                }
            }
        }

        public void SetMainPage(string page, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = GetCachedOrNewPage(page, paramService);
            NavigationProvider.TrySetNavigation(nextPage);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            ApplicationProvider.MainPage = nextPage;
            if (nextPage is NavigationPage)
            {
                PageStackController.AddPageToNavigationStack(page);
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
        }

        protected virtual Page GetCachedOrNewPage(string pageKey, IParametersService parameters)
        {
            Page page;
            var pageCacheContainer = CacheCoordinator.TryGetCachedPage(pageKey);
            if (pageCacheContainer != null)
            {
                page = pageCacheContainer.Page;
                TrySetManagers(page);
                if (!pageCacheContainer.Initialized)
                {
                    PageActionInvoker.InvokeInitialize(page, parameters);
                }
            }
            else
            {
                page = CacheCoordinator.GetNewPage(pageKey);
                TrySetManagers(page);
                PageActionInvoker.InvokeInitialize(page, parameters);
            }
            return page;
        }

        protected virtual void TrySetManagers(object pageArg)
        {
            var masterDetailPage = pageArg as MasterDetailPage;
            if (masterDetailPage != null)
            {
                var page = masterDetailPage as IMasterDetailPageProvider;
                if (page != null)
                {
                    page.Manager = GetMasterDetailPageManager(masterDetailPage);
                }
                var viewmodel = masterDetailPage.BindingContext as IMasterDetailPageProvider;
                if (viewmodel != null)
                {
                    viewmodel.Manager = GetMasterDetailPageManager(masterDetailPage);
                }
                return;
            }

            var tabbedPage = pageArg as TabbedPage;
            if (tabbedPage != null)
            {
                var page = tabbedPage as IMultiPageProvider;
                if (page != null)
                {
                    page.Manager = GetTabbedPageManager(tabbedPage);
                }
                var viewmodel = tabbedPage.BindingContext as IMultiPageProvider;
                if (viewmodel != null)
                {
                    viewmodel.Manager = GetTabbedPageManager(tabbedPage);
                }
                return;
            }

            var carouselPage = pageArg as CarouselPage;
            if (carouselPage != null)
            {
                var page = carouselPage as IMultiPageProvider;
                if (page != null)
                {
                    page.Manager = GetCarouselPageManager(carouselPage);
                }
                var viewmodel = carouselPage.BindingContext as IMultiPageProvider;
                if (viewmodel != null)
                {
                    viewmodel.Manager = GetCarouselPageManager(carouselPage);
                }
            }
        }

        protected virtual IMasterDetailPageManager GetMasterDetailPageManager(MasterDetailPage page)
        {
            return new MasterDetailPageManager(page, NavigationProvider, CacheCoordinator, PageStackController, TrySetManagers, GetCachedOrNewPage);
        }

        protected virtual IMultiPageManager GetTabbedPageManager(TabbedPage page)
        {
            return new TabbedPageManager(page, NavigationProvider, CacheCoordinator, PageStackController, TrySetManagers, GetCachedOrNewPage);
        }

        protected virtual IMultiPageManager GetCarouselPageManager(CarouselPage page)
        {
            return new CarouselPageManager(page, NavigationProvider, CacheCoordinator, PageStackController, TrySetManagers, GetCachedOrNewPage);
        }
    }
}
