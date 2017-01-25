using System.Linq;
using NUnit.Framework;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;
using Library.Tests.Mocks;
using Xamarin.Forms;

namespace Library.Tests.Fixtures
{
    [TestFixture]
    public class NavigationServiceFixture
    {
        [Test]
        public void SetMainPage_PageIsRegistered_MainPageIsSet()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("NavigationPage/MainPage");
            var mainPageContainer = navigationService.NavigationStack[0];
            Assert.That(mainPageContainer.Key, Is.EqualTo("MainPage"));
        }

        [Test]
        public void SetMainPage_PageIsNotRegistered_MainPageIsNotSet()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("NavigationPage/NoPage");
            var count = navigationService.NavigationStack.Count;
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void PushAsync_PageIsRegistered_PageIsPushedToStack()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("NavigationPage/MainPage");
            navigationService.PushAsync("FirstPage").Wait();
            navigationService.PushAsync("SecondPage").Wait();
            var mainPageContainer = navigationService.NavigationStack[0];
            var firstPageContainer = navigationService.NavigationStack[1];
            var secondPageContainer = navigationService.NavigationStack[2];
            Assert.That(mainPageContainer.Key, Is.EqualTo("MainPage"));
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(secondPageContainer.Key, Is.EqualTo("SecondPage"));
        }

        [Test]
        public void PushModalAsync_PageIsRegistered_PageIsPushedToStack()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("MainPage");
            navigationService.PushModalAsync("FirstPage").Wait();
            navigationService.PushModalAsync("SecondPage").Wait();
            var firstPageContainer = navigationService.ModalStack[0];
            var secondPageContainer = navigationService.ModalStack[1];
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(secondPageContainer.Key, Is.EqualTo("SecondPage"));
        }

        [Test]
        public void PopAsync_PageIsRegistered_PageIsPoppedFromStack()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("NavigationPage/MainPage");
            navigationService.PushAsync("FirstPage").Wait();
            navigationService.PushAsync("SecondPage").Wait();
            var mainPageContainer = navigationService.NavigationStack[0];
            var firstPageContainer = navigationService.NavigationStack[1];
            var secondPageContainer = navigationService.NavigationStack[2];
            Assert.That(mainPageContainer.Key, Is.EqualTo("MainPage"));
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(secondPageContainer.Key, Is.EqualTo("SecondPage"));
            navigationService.PopAsync().Wait();
            Assert.That(navigationService.NavigationStack.Count, Is.EqualTo(2));
            var lastPage = navigationService.NavigationStack[1];
            Assert.That(lastPage.Key, Is.EqualTo("FirstPage"));
            navigationService.PopAsync().Wait();
            lastPage = navigationService.NavigationStack[0];
            Assert.That(lastPage.Key, Is.EqualTo("MainPage"));
        }

        [Test]
        public void PopModalAsync_PageIsRegistered_PageIsPoppedFromStack()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("MainPage");
            navigationService.PushModalAsync("FirstPage").Wait();
            navigationService.PushModalAsync("SecondPage").Wait();
            var firstPageContainer = navigationService.ModalStack[0];
            var secondPageContainer = navigationService.ModalStack[1];
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(secondPageContainer.Key, Is.EqualTo("SecondPage"));
            navigationService.PopModalAsync().Wait();
            Assert.That(navigationService.ModalStack.Count, Is.EqualTo(1));
            var lastPage = navigationService.ModalStack[0];
            Assert.That(lastPage.Key, Is.EqualTo("FirstPage"));
        }

        protected static INavigationService GetNavigationService()
        {
            return new NavigationServiceMock();
        }

        protected static void Setup()
        {
            var pageType = typeof(ContentPage);
            PageNavigationStore.Current.PageTypes["NavigationPage"] = typeof(NavigationPage);
            PageNavigationStore.Current.PageTypes["MainPage"] = pageType;
            PageNavigationStore.Current.PageTypes["FirstPage"] = pageType;
            PageNavigationStore.Current.PageTypes["SecondPage"] = pageType;
            PageNavigationStore.Current.PageTypes["ThirdPage"] = pageType;
        }
    }
}
