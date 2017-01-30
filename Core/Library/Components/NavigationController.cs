using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class NavigationController : INavigationController
    {
        protected IApplicationProvider ApplicationProvider { get; set; }

        protected INavigationProvider NavigationProvider { get; set; }

        protected IPageStackController PageStackController { get; set; }

        public NavigationController(IApplicationProvider applicationProvider,
                                    INavigationProvider navigationProvider,
                                    IPageStackController pageStackController)
        {
            ApplicationProvider = applicationProvider;
            NavigationProvider = navigationProvider;
            PageStackController = pageStackController;
        }

        public virtual void SetMainPage(string pageKey, object page, IParametersService parameters)
        {
            var pageInstance = page as Page;
            if (pageInstance != null)
            {
                if (pageInstance is NavigationPage)
                {
                    PageStackController.AddPageToNavigationStack(pageKey);
                }
                NavigationProvider.TrySetNavigation(pageInstance);
                PageActionInvoker.InvokeOnPageAppearing(pageInstance, parameters);
                ApplicationProvider.MainPage = pageInstance;
                PageActionInvoker.InvokeOnPageAppeared(pageInstance, parameters);
            }
        }

        public virtual object GetMainPage()
        {
            return ApplicationProvider.MainPage;
        }

        public virtual async Task PushPageAsync(string pageKey, object page, bool animated, IParametersService parameters, bool useModal)
        {
            var pageStack = useModal ? PageStackController.ModalStack
                                     : PageStackController.NavigationStack;
            var nextPage = page as Page;
            PageActionInvoker.InvokeOnPageAppearing(nextPage, parameters);
            if (useModal)
            {
                await NavigationProvider.Navigation.PushModalAsync(nextPage, animated);
                PageStackController.AddPageToModalStack(pageKey);
            }
            else
            {
                await NavigationProvider.Navigation.PushAsync(nextPage, animated);
                PageStackController.AddPageToNavigationStack(pageKey);
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, parameters);
        }

        public virtual async Task<IPageContainer> PopPageAsync(bool animated, IParametersService parameters, bool useModal)
        {
            var pageStack = useModal ? PageStackController.ModalStack
                                     : PageStackController.NavigationStack;
            var currentPage = pageStack[pageStack.Count - 1];
            PageActionInvoker.InvokeOnPageDisappearing(currentPage, parameters);
            IPageContainer pageContainer;
            if (useModal)
            {
                await NavigationProvider.Navigation.PopModalAsync(animated);
                pageContainer = PageStackController.PopPageFromModalStack();
            }
            else
            {
                await NavigationProvider.Navigation.PopAsync(animated);
                pageContainer = PageStackController.PopPageFromNavigationStack();
            }
            PageActionInvoker.InvokeOnPageDisappeared(currentPage, parameters);
            return pageContainer;
        }

        public virtual void InsertPageBefore(string pageKey, object page, string before, IParametersService parameters)
        {
            var pageInstance = page as Page;
            if (pageInstance != null)
            {
                var beforeIndex = PageStackController.NavigationStack.IndexOf(PageStackController.NavigationStack.FirstOrDefault(x => x.Key == before));
                var beforePage = NavigationProvider.Navigation.NavigationStack.ElementAtOrDefault(beforeIndex);
                NavigationProvider.Navigation.InsertPageBefore(pageInstance, beforePage);
                PageStackController.NavigationStack.Insert(beforeIndex, new PageContainer(pageKey, pageInstance.GetType()));
            }
        }

        public virtual async Task PopToRootAsync(bool animated, IParametersService parameters)
        {
            while (NavigationProvider.Navigation.NavigationStack.Count > 1)
            {
                await PopPageAsync(animated, parameters, false);
            }
        }

        public virtual void RemovePage(string pageKey)
        {
            var navigationStack = PageStackController.NavigationStack;
            for (var i = navigationStack.Count - 1; i >= 0; i--)
            {
                if (navigationStack[i].Key == pageKey)
                {
                    NavigationProvider.Navigation.RemovePage(NavigationProvider.Navigation.NavigationStack[i]);
                    navigationStack.RemoveAt(i);
                    break;
                }
            }
        }

        public IReadOnlyList<IPageContainer> GetNavigationStack()
        {
            return PageStackController.NavigationStack.ToList();
        }

        public IReadOnlyList<IPageContainer> GetModalStack()
        {
            return PageStackController.ModalStack.ToList();
        }

        public INavigation GetNavigation()
        {
            return NavigationProvider.Navigation;
        }

        public void TrySetNavigation(object page)
        {
            NavigationProvider.TrySetNavigation(page);
        }

        public void AddPageToNavigationStack(string page)
        {
            PageStackController.AddPageToNavigationStack(page);
        }
    }
}
