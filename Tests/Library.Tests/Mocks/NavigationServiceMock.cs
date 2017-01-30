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
        public NavigationServiceMock(INavigationController navigationController, IPageCacheController pageCacheController)
            : base(navigationController, pageCacheController) { }
    }
}
