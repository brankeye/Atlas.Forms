using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
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
            var nextPage = page as Page;
            PageActionInvoker.InvokeOnPageAppearing(nextPage, parameters);
            if (useModal)
            {
                await NavigationProvider.Navigation.PushModalAsync(nextPage, animated);
            }
            else
            {
                await NavigationProvider.Navigation.PushAsync(nextPage, animated);
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, parameters);
        }

        public virtual async Task<IPageContainer> PopPageAsync(bool animated, IParametersService parameters, bool useModal)
        {
            var pageStack = useModal ? NavigationProvider.Navigation.ModalStack
                                     : NavigationProvider.Navigation.NavigationStack;
            var currentPage = pageStack[pageStack.Count - 1];
            var pageContainer = PageKeyStore.Current.GetPageContainer(currentPage);
            PageActionInvoker.InvokeOnPageDisappearing(currentPage, parameters);
            if (useModal)
            {
                await NavigationProvider.Navigation.PopModalAsync(animated);
            }
            else
            {
                await NavigationProvider.Navigation.PopAsync(animated);
            }
            PageActionInvoker.InvokeOnPageDisappeared(currentPage, parameters);
            return pageContainer;
        }

        public virtual void InsertPageBefore(string pageKey, object page, string before, IParametersService parameters)
        {
            var pageInstance = page as Page;
            if (pageInstance != null)
            {
                var navStack = PageStackController.NavigationStack.ToList();
                var beforeElement = navStack.FirstOrDefault(x => x.Key == before);
                var beforeIndex = navStack.IndexOf(beforeElement);
                var beforePage = NavigationProvider.Navigation.NavigationStack.ElementAtOrDefault(beforeIndex);
                NavigationProvider.Navigation.InsertPageBefore(pageInstance, beforePage);
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
                    break;
                }
            }
        }

        public virtual IReadOnlyList<IPageContainer> GetNavigationStack()
        {
            return PageStackController.NavigationStack.ToList();
        }

        public virtual IReadOnlyList<IPageContainer> GetModalStack()
        {
            return PageStackController.ModalStack.ToList();
        }

        public virtual INavigation GetNavigation()
        {
            return NavigationProvider.Navigation;
        }

        public virtual void TrySetNavigation(object page)
        {
            NavigationProvider.TrySetNavigation(page);
        }
    }
}
