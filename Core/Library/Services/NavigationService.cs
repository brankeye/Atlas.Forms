using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class NavigationService : INavigationService
    {
        public static INavigationService Current { get; internal set; }

        public IReadOnlyList<IPageContainer> NavigationStack => NavigationStackInternal.ToList();

        public IReadOnlyList<IPageContainer> ModalStack => ModalStackInternal.ToList();

        protected IList<IPageContainer> NavigationStackInternal { get; } = new List<IPageContainer>();

        protected IList<IPageContainer> ModalStackInternal { get; } = new List<IPageContainer>();

        public INavigation Navigation { get; set; }

        protected IApplicationProvider ApplicationProvider { get; }

        protected IPageCacheCoordinator CacheCoordinator { get; }

        public NavigationService(IApplicationProvider applicationProvider, IPageCacheCoordinator cacheCoordinator)
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
                                          navigation => navigation.PopAsync(animated),
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

        protected virtual async Task<IPageContainer> PopInternalAsync(IList<IPageContainer> stackInternal, IReadOnlyList<Page> pageStack, Func<INavigation, Task> func, bool animated, IParametersService parameters = null)
        {
            if (stackInternal.Count > 0)
            {
                CacheCoordinator.RemoveCachedPages(stackInternal[stackInternal.Count - 1].Key);
            }
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
            SetNavigation(nextPage);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            await func(Navigation, nextPage);
            AddPageToStack(page, nextPage, stackInternal);
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
        }

        public virtual void PresentPage(object currentPage, string page, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page, paramService);
            SetNavigation(nextPage);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            if (currentPage is MasterDetailPage)
            {
                ((MasterDetailPage) currentPage).Detail = nextPage;
            }
            else if (currentPage is TabbedPage)
            {
                var tabbedPage = (TabbedPage) currentPage;
                tabbedPage.CurrentPage = tabbedPage.Children.FirstOrDefault(x => x.Title == page);
            }
            if (nextPage is NavigationPage)
            {
                AddPageToStack(page, nextPage, NavigationStackInternal);
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
            SetNavigation(nextPage);
            ApplicationProvider.MainPage = nextPage;
            if (nextPage is NavigationPage)
            {
                AddPageToStack(page, nextPage, NavigationStackInternal);
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            CacheCoordinator.LoadCachedPages(page, CacheOption.Appears);
        }

        protected virtual void AddPageToStack(string pageKey, Page page, IList<IPageContainer> stack)
        {
            if (PageKeyParser.IsSequence(pageKey))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(pageKey);
                queue.Dequeue();
                var innerPageKey = queue.Dequeue();
                Type innerPageType;
                PageNavigationStore.Current.PageTypes.TryGetValue(innerPageKey, out innerPageType);
                if (innerPageType == null)
                {
                    return;
                }
                stack.Add(new PageContainer(innerPageKey, innerPageType));
            }
            else
            {
                stack.Add(new PageContainer(pageKey, PageNavigationStore.Current.PageTypes[pageKey]));
            }
        }

        protected virtual void SetNavigation(Page page)
        {
            if (page is NavigationPage)
            {
                Navigation = page.Navigation;
            }
            else if (Navigation == null)
            {
                Navigation = page.Navigation;
            }
        }
    }
}
