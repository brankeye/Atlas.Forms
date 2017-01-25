using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Services;
using Library.Tests.Helpers;
using Moq;
using Xamarin.Forms;

namespace Library.Tests.Mocks
{
    public class NavigationServiceMock : NavigationService
    {
        public NavigationServiceMock() 
            : base(CreateApplicationProviderMock(), CreatePageCacheCoordinatorMock()) {}

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

        protected static IPageCacheCoordinator CreatePageCacheCoordinatorMock()
        {
            StateManager.ResetAll();
            return new PageCacheCoordinator();
        }
    }
}
