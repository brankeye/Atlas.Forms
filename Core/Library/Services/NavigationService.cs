using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Services
{
    public class NavigationService : INavigationService
    {
        public static INavigationService Current { get; private set; }

        public IReadOnlyList<IPageContainer> NavigationStack => NavigationStackInternal.ToList();

        public IReadOnlyList<IPageContainer> ModalStack => ModalStackInternal.ToList();

        protected IList<IPageContainer> NavigationStackInternal { get; } = new List<IPageContainer>();

        protected IList<IPageContainer> ModalStackInternal { get; } = new List<IPageContainer>();

        protected INavigation Navigation { get; set; }

        protected IApplicationProvider ApplicationProvider { get; }

        protected IPageFactory PageFactory { get; set; }

        public NavigationService(IApplicationProvider applicationProvider, IPageFactory pageFactory, bool setCurrent = false)
        {
            ApplicationProvider = applicationProvider;
            PageFactory = pageFactory;
            if (Current == null || setCurrent) Current = this;
        }

        public void InsertPageBefore(string page, string before)
        {
            var nextPage = PageFactory.CreatePage(page);
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
            PageCacheStore.RemovePages(PageCacheMap.GetCachedPages(NavigationStackInternal.Last().Key));
            var currentPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            PageMethodInvoker.InvokeOnPageDisappearing(currentPage);
            await Navigation.PopAsync(animated);
            var pageContainer = NavigationStackInternal.Last();
            NavigationStackInternal.Remove(pageContainer);
            PageMethodInvoker.InvokeOnPageDisappeared(currentPage);
            PageCache.PreloadCachedPages(NavigationStackInternal.Last().Key);
            return pageContainer;
        }

        public async Task<IPageContainer> PopModalAsync()
        {
            var pageContainer = await PopModalAsync(true);
            return pageContainer;
        }

        public async Task<IPageContainer> PopModalAsync(bool animated)
        {
            PageCacheStore.RemovePages(PageCacheMap.GetCachedPages(ModalStackInternal.Last().Key));
            var currentPage = Navigation.ModalStack[Navigation.ModalStack.Count - 1];
            PageMethodInvoker.InvokeOnPageDisappearing(currentPage);
            await Navigation.PopModalAsync(animated);
            var pageContainer = ModalStackInternal.Last();
            ModalStackInternal.Remove(pageContainer);
            PageMethodInvoker.InvokeOnPageDisappeared(currentPage);
            PageCache.PreloadCachedPages(ModalStackInternal.Last().Key);
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

        public async Task PushAsync(string page)
        {
            await PushAsync(page, true);
        }

        public async Task PushAsync(string page, bool animated)
        {
            PageCacheStore.RemovePages(PageCacheMap.GetCachedPages(NavigationStackInternal.Last().Key));
            var nextPage = PageRetriever.GetPage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage);
            await Navigation.PushAsync(nextPage, animated);
            NavigationStackInternal.Add(new PageContainer(page, nextPage.GetType()));
            PageMethodInvoker.InvokeOnPageAppeared(nextPage);
            
        }

        public async Task PushModalAsync(string page)
        {
            await PushModalAsync(page, true);
        }

        public async Task PushModalAsync(string page, bool animated)
        {
            PageCacheStore.RemovePages(PageCacheMap.GetCachedPages(ModalStackInternal.Last().Key));
            var nextPage = PageRetriever.GetPage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage);
            await Navigation.PushModalAsync(nextPage, animated);
            PageMethodInvoker.InvokeOnPageAppeared(nextPage);
            ModalStackInternal.Add(new PageContainer(page, nextPage.GetType()));
        }

        public void RemovePage(string page)
        {
            // TODO: loop through NavigationStack to find page with same key, then find actual page reference to remove.
            var nextPage = PageFactory.CreatePage(page);
            Navigation.RemovePage(nextPage);
            NavigationStackInternal.RemoveAt(NavigationStackInternal.Count - 1);
        }

        public void SetMainPage(string page)
        {
            var nextPage = PageRetriever.GetPage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage);
            var navigationPage = nextPage as NavigationPage;
            if (navigationPage != null)
            {
                NavigationStackInternal.Add(new PageContainer(page, navigationPage.GetType()));
            }
            ApplicationProvider.MainPage = nextPage;
            PageMethodInvoker.InvokeOnPageAppeared(nextPage);
            if (ApplicationProvider.MainPage != null)
            {
                Navigation = ApplicationProvider.MainPage.Navigation;
            }
        }
    }
}
