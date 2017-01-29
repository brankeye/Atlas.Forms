using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Services;
using Library.Tests.Helpers;
using Moq;
using Xamarin.Forms;

namespace Library.Tests.Mocks
{
    public class NavigationServiceMock : NavigationService
    {
        public NavigationServiceMock() 
            : base(CreateNavigationController(), CreatePageCacheController()) {}

        protected static IApplicationProvider CreateApplicationProviderMock()
        {
            var mock = new Mock<IApplicationProvider>();
            var mainPage = new ContentPage
            {
                Title = "MainPage"
            };
            mock.SetupSet(x => x.MainPage = mainPage);
            return mock.Object;
        }

        protected static INavigationController CreateNavigationController()
        {
            return new NavigationController(new ApplicationProvider(), new NavigationProvider(), new PageStackController());
        }

        protected static IPageCacheController CreatePageCacheController()
        {
            StateManager.ResetAll();
            return new PageCacheController(new PageFactory(), new CacheController());
        }
    }
}
