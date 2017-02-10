using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class NavigationController : INavigationController
    {
        protected IApplicationProvider ApplicationProvider { get; }

        protected INavigationProvider NavigationProvider { get; }

        protected IPageStackController PageStackController { get; }

        protected IPageKeyStore PageKeyStore { get; }
        
        public NavigationController(IApplicationProvider applicationProvider,
                                    INavigationProvider navigationProvider,
                                    IPageStackController pageStackController,
                                    IPageKeyStore pageKeyStore)
        {
            ApplicationProvider = applicationProvider;
            NavigationProvider = navigationProvider;
            PageStackController = pageStackController;
            PageKeyStore = pageKeyStore;
        }

        public virtual void SetMainPage(Page page, IParametersService parameters)
        {
            if (page != null)
            {
                NavigationProvider.TrySetNavigation(page);
                PageActionInvoker.InvokeOnPageAppearing(page, parameters);
                ApplicationProvider.MainPage = page;
                PageActionInvoker.InvokeOnPageAppeared(page, parameters);
            }
        }

        public virtual Page GetMainPage()
        {
            return ApplicationProvider.MainPage;
        }

        public virtual async Task PushPageAsync(Page page, bool animated, IParametersService parameters, bool useModal)
        {
            if (page != null)
            {
                PageActionInvoker.InvokeOnPageAppearing(page, parameters);
                if (useModal)
                {
                    await NavigationProvider.Navigation.PushModalAsync(page, animated);
                }
                else
                {
                    await NavigationProvider.Navigation.PushAsync(page, animated);
                }
                PageActionInvoker.InvokeOnPageAppeared(page, parameters);
            }
        }

        public virtual async Task<IPageInfo> PopPageAsync(bool animated, IParametersService parameters, bool useModal)
        {
            var pageStack = useModal ? NavigationProvider.Navigation.ModalStack
                                     : NavigationProvider.Navigation.NavigationStack;
            var currentPage = pageStack[pageStack.Count - 1];
            var pageContainer = PageKeyStore.GetPageContainer(currentPage);
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

        public virtual void InsertPageBefore(Page page, string before, IParametersService parameters)
        {
            if (page != null)
            {
                var navStack = PageStackController.NavigationStack.ToList();
                var beforeElement = navStack.FirstOrDefault(x => x.Key == before);
                var beforeIndex = navStack.IndexOf(beforeElement);
                var beforePage = NavigationProvider.Navigation.NavigationStack.ElementAtOrDefault(beforeIndex);
                NavigationProvider.Navigation.InsertPageBefore(page, beforePage);
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

        public virtual IReadOnlyList<IPageInfo> GetNavigationStack()
        {
            return PageStackController.NavigationStack.ToList();
        }

        public virtual IReadOnlyList<IPageInfo> GetModalStack()
        {
            return PageStackController.ModalStack.ToList();
        }

        public virtual INavigation GetNavigation()
        {
            return NavigationProvider.Navigation;
        }

        public virtual void TrySetNavigation(Page page)
        {
            NavigationProvider.TrySetNavigation(page);
        }
    }
}
