using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

        public NavigationService(IApplicationProvider applicationProvider, IPageFactory pageFactory)
        {
            ApplicationProvider = applicationProvider;
            PageFactory = pageFactory;
            if (Current == null) Current = this;
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
            var currentPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            PageMethodInvoker.InvokeOnPageDisappearing(currentPage);
            await Navigation.PopAsync(animated);
            var pageContainer = NavigationStackInternal.Last();
            NavigationStackInternal.Remove(pageContainer);
            PageMethodInvoker.InvokeOnPageDisappeared(currentPage);
            return pageContainer;
        }

        public async Task<IPageContainer> PopModalAsync()
        {
            var pageContainer = await PopModalAsync(true);
            return pageContainer;
        }

        public async Task<IPageContainer> PopModalAsync(bool animated)
        {
            var currentPage = Navigation.ModalStack[Navigation.ModalStack.Count - 1];
            PageMethodInvoker.InvokeOnPageDisappearing(currentPage);
            await Navigation.PopModalAsync(animated);
            var pageContainer = ModalStackInternal.Last();
            ModalStackInternal.Remove(pageContainer);
            PageMethodInvoker.InvokeOnPageDisappeared(currentPage);
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
            var nextPage = PageFactory.CreatePage(page);
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
            var nextPage = PageFactory.CreatePage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage);
            await Navigation.PushModalAsync(nextPage, animated);
            PageMethodInvoker.InvokeOnPageAppeared(nextPage);
            ModalStackInternal.Add(new PageContainer(page, nextPage.GetType()));
        }

        public void RemovePage(string page)
        {
            var nextPage = PageFactory.CreatePage(page);
            Navigation.RemovePage(nextPage);
            NavigationStackInternal.RemoveAt(NavigationStackInternal.Count - 1);
        }

        public void SetMainPage(string page)
        {
            var nextPage = PageFactory.CreatePage(page);
            PageMethodInvoker.InvokeOnPageAppearing(nextPage);
            var navigationPage = nextPage as NavigationPage;
            if (navigationPage != null)
            {
                var typeInfo = navigationPage.GetType().GetTypeInfo();
                //var hasConstructorThatTakesPage = typeInfo.DeclaredConstructors.Any(x => x.GetParameters().Any(y => y.ParameterType == typeof(Page)));
                NavigationStackInternal.Add(new PageContainer(page, navigationPage.GetType()));
                //navigationPage.Popped += NavigationPage_OnPopped;
            }
            ApplicationProvider.MainPage = nextPage;
            PageMethodInvoker.InvokeOnPageAppeared(nextPage);
            if (ApplicationProvider.MainPage != null)
            {
                Navigation = ApplicationProvider.MainPage.Navigation;
            }
        }

        /*
        private void NavigationPage_OnPopped(object sender, NavigationEventArgs navigationEventArgs)
        {
            var navigationPage = (NavigationPage) sender;
            var currentPage = navigationPage.CurrentPage;
            var previousPage = navigationEventArgs.Page;
            PageMethodInvoker.InvokeOnNavigatingFrom(previousPage);
            NavigationStackInternal.RemoveAt(NavigationStackInternal.Count - 1);
            PageMethodInvoker.InvokeOnNavigatedFrom(previousPage);
            PageMethodInvoker.InvokeOnNavigatingTo(currentPage);
            PageMethodInvoker.InvokeOnNavigatedTo(currentPage);
            previousPage.Behaviors?.Clear();

            if (Navigation.NavigationStack?.Count == 0)
            {
                navigationPage.Popped -= NavigationPage_OnPopped;
            }
        }
        */
    }
}
