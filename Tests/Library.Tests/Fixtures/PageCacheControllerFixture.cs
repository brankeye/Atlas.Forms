using System.Collections.Generic;
using NUnit.Framework;
using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Atlas.Forms.Services;
using Library.Tests.Helpers;
using Xamarin.Forms;

namespace Library.Tests.Fixtures
{
    [TestFixture]
    public class PageCacheControllerFixture
    {
        [Test]
        public void GetCachedOrNewPage_PageNotRegistered_ReturnsNull()
        {
            var cacheCoordinator = GetPageCacheController();
            var page = cacheCoordinator.TryGetCachedPage("MainPage", new ParametersService());
            Assert.That(page, Is.Null);
        }

        [Test]
        public void GetCachedOrNewPage_PageRegistered_ReturnsPage()
        {
            var cacheCoordinator = GetPageCacheController();
            PageNavigationStore.Current.PageTypes["MainPage"] = typeof(ContentPage);
            var page = cacheCoordinator.TryGetCachedPage("MainPage", new ParametersService());
            Assert.That(page, Is.Null);
        }

        [Test]
        public void GetCachedOrNewPage_PageExists_ReturnsPage()
        {
            var cacheCoordinator = GetPageCacheController();
            PageCacheStore.Current.PageCache["MainPage"] = new PageCacheContainer
            {
                Key = "MainPage",
                Page = new ContentPage()
            };
            var page = cacheCoordinator.TryGetCachedPage("MainPage", new ParametersService());
            Assert.That(page, Is.Not.Null);
        }

        [Test]
        public void AddCachedPages_PagesLoaded_PagesAreLoaded()
        {
            var cacheCoordinator = GetPageCacheController();
            PageNavigationStore.Current.AddTypeAndConstructorInfo("FirstPage", typeof(ContentPage));
            PageNavigationStore.Current.AddTypeAndConstructorInfo("SecondPage", typeof(ContentPage));
            PageNavigationStore.Current.AddTypeAndConstructorInfo("ThirdPage", typeof(ContentPage));
            var list = new List<PageMapContainer>
            {
                new PageMapContainer
                {
                    Key = "FirstPage",
                    Type = typeof(ContentPage),
                    CacheOption = CacheOption.Appears,
                    CacheState = CacheState.Default
                },
                new PageMapContainer
                {
                    Key = "SecondPage",
                    Type = typeof(ContentPage),
                    CacheOption = CacheOption.Appears,
                    CacheState = CacheState.KeepAlive
                },
                new PageMapContainer
                {
                    Key = "ThirdPage",
                    Type = typeof(ContentPage),
                    CacheOption = CacheOption.Appears,
                    CacheState = CacheState.SingleInstance
                }
            };
            PageCacheMap.Current.Mappings["MainPage"] = list;
            cacheCoordinator.AddCachedPages("MainPage");
            PageCacheContainer pageOne, pageTwo, pageThree;
            PageCacheStore.Current.PageCache.TryGetValue("FirstPage", out pageOne);
            PageCacheStore.Current.PageCache.TryGetValue("SecondPage", out pageTwo);
            PageCacheStore.Current.PageCache.TryGetValue("ThirdPage", out pageThree);
            Assert.That(pageOne, Is.Not.Null);
            Assert.That(pageTwo, Is.Not.Null);
            Assert.That(pageThree, Is.Not.Null);
        }

        [Test]
        public void RemoveCachedPages_PagesExist_RemovesAllButSingleInstance()
        {
            var cacheCoordinator = GetPageCacheController();
            var list = new List<PageMapContainer>
            {
                new PageMapContainer
                {
                    Key = "FirstPage",
                    Type = typeof(ContentPage),
                    CacheOption = CacheOption.Appears,
                    CacheState = CacheState.Default
                },
                new PageMapContainer
                {
                    Key = "SecondPage",
                    Type = typeof(ContentPage),
                    CacheOption = CacheOption.Appears,
                    CacheState = CacheState.KeepAlive
                },
                new PageMapContainer
                {
                    Key = "ThirdPage",
                    Type = typeof(ContentPage),
                    CacheOption = CacheOption.Appears,
                    CacheState = CacheState.SingleInstance
                }
            };
            PageCacheMap.Current.Mappings["MainPage"] = list;
            PageCacheStore.Current.PageCache["FirstPage"] = new PageCacheContainer
            {
                Page = new ContentPage(),
                Type = typeof(ContentPage),
                CacheOption = CacheOption.Appears,
                CacheState = CacheState.Default
            };
            PageCacheStore.Current.PageCache["SecondPage"] = new PageCacheContainer
            {
                Page = new ContentPage(),
                Type = typeof(ContentPage),
                CacheOption = CacheOption.Appears,
                CacheState = CacheState.KeepAlive
            };
            PageCacheStore.Current.PageCache["ThirdPage"] = new PageCacheContainer
            {
                Page = new ContentPage(),
                Type = typeof(ContentPage),
                CacheOption = CacheOption.Appears,
                CacheState = CacheState.SingleInstance
            };
            cacheCoordinator.RemoveCachedPages("MainPage");
            PageCacheContainer pageOne, pageTwo, pageThree;
            PageCacheStore.Current.PageCache.TryGetValue("FirstPage", out pageOne);
            PageCacheStore.Current.PageCache.TryGetValue("SecondPage", out pageTwo);
            PageCacheStore.Current.PageCache.TryGetValue("ThirdPage", out pageThree);
            Assert.That(pageOne, Is.Null);
            Assert.That(pageTwo, Is.Null);
            Assert.That(pageThree, Is.Not.Null);
        }

        public static IPageCacheController GetPageCacheController()
        {
            StateManager.ResetAll();
            var navigationProvider = new NavigationProvider();
            return new PageCacheController(new CacheController(), new NavigationController(new ApplicationProvider(), navigationProvider, new PageStackController(navigationProvider)));
        }
    }
}
