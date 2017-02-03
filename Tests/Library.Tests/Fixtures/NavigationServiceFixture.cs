using System.Linq;
using Atlas.Forms.Components;
using NUnit.Framework;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Library.Tests.Helpers;
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
            var stackCount = navigationService.Navigation.NavigationStack.Count;
            Assert.That(stackCount, Is.EqualTo(3));
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
            var stackCount = navigationService.Navigation.ModalStack.Count;
            Assert.That(stackCount, Is.EqualTo(2));
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
            var stackCount = navigationService.Navigation.NavigationStack.Count;
            Assert.That(stackCount, Is.EqualTo(2));
            Assert.That(navigationService.NavigationStack.Count, Is.EqualTo(2));
            var lastPage = navigationService.NavigationStack[1];
            Assert.That(lastPage.Key, Is.EqualTo("FirstPage"));
            navigationService.PopAsync().Wait();
            lastPage = navigationService.NavigationStack[0];
            Assert.That(lastPage.Key, Is.EqualTo("MainPage"));
            stackCount = navigationService.Navigation.NavigationStack.Count;
            Assert.That(stackCount, Is.EqualTo(1));
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
            var stackCount = navigationService.Navigation.ModalStack.Count;
            Assert.That(stackCount, Is.EqualTo(1));
        }

        [Test]
        public void InsertPageBefore_PageIsRegistered_PageIsInserted()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("NavigationPage/MainPage");
            navigationService.PushAsync("FirstPage").Wait();
            navigationService.PushAsync("ThirdPage").Wait();
            var mainPageContainer = navigationService.NavigationStack[0];
            var firstPageContainer = navigationService.NavigationStack[1];
            var thirdPageContainer = navigationService.NavigationStack[2];
            Assert.That(mainPageContainer.Key, Is.EqualTo("MainPage"));
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(thirdPageContainer.Key, Is.EqualTo("ThirdPage"));
            navigationService.InsertPageBefore("SecondPage", "ThirdPage");
            firstPageContainer = navigationService.NavigationStack[1];
            var secondPageContainer = navigationService.NavigationStack[2];
            thirdPageContainer = navigationService.NavigationStack[3];
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(secondPageContainer.Key, Is.EqualTo("SecondPage"));
            Assert.That(thirdPageContainer.Key, Is.EqualTo("ThirdPage"));
            var stackCount = navigationService.Navigation.NavigationStack.Count;
            Assert.That(stackCount, Is.EqualTo(4));
        }

        [Test]
        public void RemovePage_PageIsRegistered_PageIsRemoved()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("NavigationPage/MainPage");
            navigationService.PushAsync("FirstPage").Wait();
            navigationService.PushAsync("SecondPage").Wait();
            navigationService.PushAsync("ThirdPage").Wait();
            var mainPageContainer = navigationService.NavigationStack[0];
            var firstPageContainer = navigationService.NavigationStack[1];
            var secondPageContainer = navigationService.NavigationStack[2];
            var thirdPageContainer = navigationService.NavigationStack[3];
            Assert.That(mainPageContainer.Key, Is.EqualTo("MainPage"));
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(secondPageContainer.Key, Is.EqualTo("SecondPage"));
            Assert.That(thirdPageContainer.Key, Is.EqualTo("ThirdPage"));
            navigationService.RemovePage("ThirdPage");
            Assert.That(navigationService.NavigationStack.Last().Key, Is.EqualTo("SecondPage"));
            navigationService.PushAsync("ThirdPage").Wait();
            thirdPageContainer = navigationService.NavigationStack[3];
            Assert.That(thirdPageContainer.Key, Is.EqualTo("ThirdPage"));
            navigationService.RemovePage("SecondPage");
            Assert.That(navigationService.NavigationStack[1].Key, Is.EqualTo("FirstPage"));
            Assert.That(navigationService.NavigationStack[2].Key, Is.EqualTo("ThirdPage"));
            var stackCount = navigationService.Navigation.NavigationStack.Count;
            Assert.That(stackCount, Is.EqualTo(3));
        }

        [Test]
        public void PopToRootAsync_NavigationStackIsPopulated_RootIsPoppedTo()
        {
            var navigationService = GetNavigationService();
            Setup();
            navigationService.SetMainPage("NavigationPage/MainPage");
            navigationService.PushAsync("FirstPage").Wait();
            navigationService.PushAsync("SecondPage").Wait();
            navigationService.PushAsync("ThirdPage").Wait();
            var mainPageContainer = navigationService.NavigationStack[0];
            var firstPageContainer = navigationService.NavigationStack[1];
            var secondPageContainer = navigationService.NavigationStack[2];
            var thirdPageContainer = navigationService.NavigationStack[3];
            Assert.That(mainPageContainer.Key, Is.EqualTo("MainPage"));
            Assert.That(firstPageContainer.Key, Is.EqualTo("FirstPage"));
            Assert.That(secondPageContainer.Key, Is.EqualTo("SecondPage"));
            Assert.That(thirdPageContainer.Key, Is.EqualTo("ThirdPage"));
            navigationService.PopToRootAsync().Wait();
            mainPageContainer = navigationService.NavigationStack[0];
            Assert.That(mainPageContainer.Key, Is.EqualTo("MainPage"));
            Assert.That(navigationService.NavigationStack.Count, Is.EqualTo(1));
            var stackCount = navigationService.Navigation.NavigationStack.Count;
            Assert.That(stackCount, Is.EqualTo(1));
        }

        protected static INavigationService GetNavigationService()
        {
            var navigationController = new NavigationController(new ApplicationProviderMock(), new NavigationProvider(), new PageStackController());
            var pageCacheController = new PageCacheController(new CacheController(), navigationController);
            return new NavigationServiceMock(navigationController, pageCacheController);
        }

        protected static void Setup()
        {
            StateManager.ResetAll();
            var pageType = typeof(ContentPage);
            PageNavigationStore.Current.AddTypeAndConstructorInfo("NavigationPage", typeof(NavigationPage));
            PageNavigationStore.Current.AddTypeAndConstructorInfo("MainPage", pageType);
            PageNavigationStore.Current.AddTypeAndConstructorInfo("FirstPage", pageType);
            PageNavigationStore.Current.AddTypeAndConstructorInfo("SecondPage", pageType);
            PageNavigationStore.Current.AddTypeAndConstructorInfo("ThirdPage", pageType);
        }
    }
}
