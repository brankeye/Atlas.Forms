using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atlas.core.Library.Enums;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Services
{
    public class NavigationService : INavigationService
    {
        public static INavigationService Current { get; internal set; }

        public IReadOnlyList<IPageContainer> NavigationStack => NavigationStackInternal.ToList();

        public IReadOnlyList<IPageContainer> ModalStack => ModalStackInternal.ToList();

        protected IList<IPageContainer> NavigationStackInternal { get; } = new List<IPageContainer>();

        protected IList<IPageContainer> ModalStackInternal { get; } = new List<IPageContainer>();

        protected INavigation Navigation { get; set; }

        protected IApplicationProvider ApplicationProvider { get; }

        protected IPageCacheCoordinator CacheCoordinator { get; }

        internal NavigationService(IApplicationProvider applicationProvider, IPageCacheCoordinator cacheCoordinator)
        {
            ApplicationProvider = applicationProvider;
            CacheCoordinator = cacheCoordinator;
        }

        public virtual void InsertPageBefore(string page, string before, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page, paramService);
            var beforeIndex = NavigationStackInternal.IndexOf(NavigationStackInternal.FirstOrDefault(x => x.Key == before));
            var beforePage = Navigation.NavigationStack.ElementAtOrDefault(beforeIndex);
            Navigation.InsertPageBefore(nextPage, beforePage);
            NavigationStackInternal.Insert(beforeIndex, new PageContainer(page, nextPage.GetType()));
        }

        public virtual async Task<IPageContainer> PopAsync(IParametersService parameters = null)
        {
            var pageContainer = await PopAsync(true);
            return pageContainer;
        }

        public virtual async Task<IPageContainer> PopAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(NavigationStackInternal, 
                                          Navigation.NavigationStack,
                                          navigation => navigation.PopModalAsync(animated),
                                          animated, parameters);
        }

        public virtual async Task<IPageContainer> PopModalAsync(IParametersService parameters = null)
        {
            var pageContainer = await PopModalAsync(true);
            return pageContainer;
        }

        public virtual async Task<IPageContainer> PopModalAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(ModalStackInternal, 
                                          Navigation.ModalStack, 
                                          navigation => navigation.PopModalAsync(animated),
                                          animated, parameters);
        }

        protected virtual async Task<IPageContainer> PopInternalAsync(IList < IPageContainer> stackInternal, IReadOnlyList<Page> pageStack, Func<INavigation, Task> func, bool animated, IParametersService parameters = null)
        {
            CacheCoordinator.RemoveCachedPages(stackInternal.Last().Key);
            var currentPage = pageStack[pageStack.Count - 1];
            PageActionInvoker.InvokeOnPageDisappearing(currentPage, parameters);
            await func(Navigation);
            var pageContainer = stackInternal.Last();
            stackInternal.Remove(pageContainer);
            PageActionInvoker.InvokeOnPageDisappeared(currentPage, parameters);
            CacheCoordinator.LoadCachedPages(stackInternal.Last().Key, CacheOption.Appears);
            return pageContainer;
        }

        public virtual async Task PopToRootAsync()
        {
            await PopToRootAsync(true);
        }

        public virtual async Task PopToRootAsync(bool animated)
        {
            while (Navigation.NavigationStack.Count > 1)
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
            await PushInternalAsync(NavigationStackInternal,
                                    (navigation, nextPage) => navigation.PushAsync(nextPage, animated),
                                    page, animated, parameters);
        }

        public virtual async Task PushModalAsync(string page, IParametersService parameters = null)
        {
            await PushModalAsync(page, true, parameters);
        }

        public virtual async Task PushModalAsync(string page, bool animated, IParametersService parameters = null)
        {
            await PushInternalAsync(ModalStackInternal, 
                                    (navigation, nextPage) => navigation.PushModalAsync(nextPage, animated),
                                    page, animated, parameters);
        }

        protected virtual async Task PushInternalAsync(IList<IPageContainer> stackInternal, Func<INavigation, Page, Task> func, string page, bool animated, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            if (stackInternal.Count > 0)
            {
                CacheCoordinator.RemoveCachedPages(stackInternal.Last().Key);
            }
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page, paramService);
            
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            await func(Navigation, nextPage);
            stackInternal.Add(new PageContainer(page, nextPage.GetType()));
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
        }

        public virtual void Present(object currentPage, string page, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page, paramService);
            if (nextPage is NavigationPage) Navigation = (nextPage as NavigationPage).Navigation;
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            if (currentPage is MasterDetailPage)
            {
                ((MasterDetailPage) currentPage).Detail = nextPage;
            }
            else if (currentPage is TabbedPage)
            {
                ((TabbedPage) currentPage).CurrentPage = nextPage;
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
        }

        public void RemovePage(string page)
        {
            for (var i = NavigationStackInternal.Count - 1; i >= 0; i--)
            {
                if (NavigationStackInternal[i].Key == page)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[i]);
                    NavigationStackInternal.RemoveAt(i);
                    break;
                }
            }
        }

        public void SetMainPage(string page, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page, paramService);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            var navigationPage = nextPage as NavigationPage;
            if (navigationPage != null)
            {
                NavigationStackInternal.Add(new PageContainer(page, navigationPage.GetType()));
            }
            Navigation = nextPage.Navigation;
            ApplicationProvider.MainPage = nextPage;
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
        }
    }
}
