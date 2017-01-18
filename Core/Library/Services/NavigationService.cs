using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;
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

        public void InsertPageBefore(string page, string before)
        {
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page);
            var beforeIndex = NavigationStackInternal.IndexOf(NavigationStackInternal.FirstOrDefault(x => x.Key == before));
            var beforePage = Navigation.NavigationStack.ElementAtOrDefault(beforeIndex);
            Navigation.InsertPageBefore(nextPage, beforePage);
            NavigationStackInternal.Insert(beforeIndex, new PageContainer(page, nextPage.GetType()));
        }

        public async Task<IPageContainer> PopAsync()
        {
            var pageContainer = await PopAsync(true);
            return pageContainer;
        }

        public async Task<IPageContainer> PopAsync(bool animated)
        {
            CacheCoordinator.RemoveCachedPages(NavigationStackInternal.Last().Key);
            var currentPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            PageMethodInvoker.InvokeOnPageDisappearing(currentPage);
            await Navigation.PopAsync(animated);
            var pageContainer = NavigationStackInternal.Last();
            NavigationStackInternal.Remove(pageContainer);
            PageMethodInvoker.InvokeOnPageDisappeared(currentPage);
            CacheCoordinator.LoadCachedPages(NavigationStackInternal.Last().Key);
            return pageContainer;
        }

        public async Task<IPageContainer> PopModalAsync()
        {
            var pageContainer = await PopModalAsync(true);
            return pageContainer;
        }

        public async Task<IPageContainer> PopModalAsync(bool animated)
        {
            CacheCoordinator.RemoveCachedPages(ModalStackInternal.Last().Key);
            var currentPage = Navigation.ModalStack[Navigation.ModalStack.Count - 1];
            PageMethodInvoker.InvokeOnPageDisappearing(currentPage);
            await Navigation.PopModalAsync(animated);
            var pageContainer = ModalStackInternal.Last();
            ModalStackInternal.Remove(pageContainer);
            PageMethodInvoker.InvokeOnPageDisappeared(currentPage);
            CacheCoordinator.LoadCachedPages(ModalStackInternal.Last().Key);
            return pageContainer;
        }

        public async Task PopToRootAsync()
        {
            await PopToRootAsync(true);
        }

        public async Task PopToRootAsync(bool animated)
        {
            while (Navigation.NavigationStack.Count > 1)
            {
                await PopAsync(animated);
            }
        }

        public async Task PushAsync(string page, IParametersService parameters = null)
        {
            await PushAsync(page, true, parameters);
        }

        public async Task PushAsync(string page, bool animated, IParametersService parameters = null)
        {
            CacheCoordinator.RemoveCachedPages(NavigationStackInternal.Last().Key);
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage, parameters);
            await Navigation.PushAsync(nextPage, animated);
            NavigationStackInternal.Add(new PageContainer(page, nextPage.GetType()));
            PageMethodInvoker.InvokeOnPageAppeared(nextPage, parameters);
            
        }

        public async Task PushModalAsync(string page, IParametersService parameters = null)
        {
            await PushModalAsync(page, true, parameters);
        }

        public async Task PushModalAsync(string page, bool animated, IParametersService parameters = null)
        {
            CacheCoordinator.RemoveCachedPages(ModalStackInternal.Last().Key);
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage, parameters);
            await Navigation.PushModalAsync(nextPage, animated);
            PageMethodInvoker.InvokeOnPageAppeared(nextPage, parameters);
            ModalStackInternal.Add(new PageContainer(page, nextPage.GetType()));
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
            var nextPage = CacheCoordinator.GetCachedOrNewPage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage, parameters);
            var navigationPage = nextPage as NavigationPage;
            if (navigationPage != null)
            {
                NavigationStackInternal.Add(new PageContainer(page, navigationPage.GetType()));
            }
            ApplicationProvider.MainPage = nextPage;
            PageMethodInvoker.InvokeOnPageAppeared(nextPage, parameters);
            if (ApplicationProvider.MainPage != null)
            {
                Navigation = ApplicationProvider.MainPage.Navigation;
            }
        }
    }
}
