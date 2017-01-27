using System.Collections.Generic;
using NUnit.Framework;
using Atlas.Forms.Caching;
using Atlas.Forms.Enums;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages.Containers;
using Library.Tests.Helpers;
using Xamarin.Forms;

namespace Library.Tests.Fixtures
{
    [TestFixture]
    public class PageCacheCoordinatorFixture
    {
        [Test]
        public void TryGetCachedPage_PageNotExists_ReturnsNull()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            var page = cacheCoordinator.TryGetCachedPage("NonExistentPage");
            Assert.That(page, Is.Null);
        }

        [Test]
        public void TryGetCachedPage_PageExists_ReturnsPage()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            PageCacheStore.Current.PageCache["MainPage"] = new PageCacheContainer
            {
                Key = "MainPage",
                Page = new ContentPage()
            };
            var page = cacheCoordinator.TryGetCachedPage("MainPage");
            Assert.That(page, Is.Not.Null);
        }

        [Test]
        public void GetCachedOrNewPage_PageNotRegistered_ReturnsNull()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            var page = cacheCoordinator.GetCachedOrNewPage("MainPage");
            Assert.That(page, Is.Null);
        }

        [Test]
        public void GetCachedOrNewPage_PageRegistered_ReturnsPage()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            PageNavigationStore.Current.PageTypes["MainPage"] = typeof(ContentPage);
            var page = cacheCoordinator.GetCachedOrNewPage("MainPage");
            Assert.That(page, Is.Not.Null);
        }

        [Test]
        public void GetCachedOrNewPage_PageExists_ReturnsPage()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            PageCacheStore.Current.PageCache["MainPage"] = new PageCacheContainer
            {
                Key = "MainPage",
                Page = new ContentPage()
            };
            var page = cacheCoordinator.GetCachedOrNewPage("MainPage");
            Assert.That(page, Is.Not.Null);
        }

        [Test]
        public void AddPageToCache_PageExists_DoesNotAdd()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            PageNavigationStore.Current.PageTypes["MainPage"] = typeof(ContentPage);
            PageCacheStore.Current.PageCache["MainPage"] = new PageCacheContainer
            {
                Key = "MainPage",
                Page = new ContentPage
                {
                    Title = "ExistingPage"
                }
            };
            var container = new PageMapContainer
            {
                Key = "MainPage",
                Type = typeof(ContentPage)
            };
            cacheCoordinator.AddPageToCache("MainPage", container);
            var page = cacheCoordinator.TryGetCachedPage("MainPage");
            Assert.That(page.Page.Title, Is.EqualTo("ExistingPage"));
        }

        [Test]
        public void AddPageToCache_PageNotExists_DoesAdd()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            PageNavigationStore.Current.PageTypes["MainPage"] = typeof(ContentPage);
            var container = new PageMapContainer
            {
                Key = "MainPage",
                Type = typeof(ContentPage)
            };
            cacheCoordinator.AddPageToCache("MainPage", container);
            var page = cacheCoordinator.TryGetCachedPage("MainPage");
            Assert.That(page, Is.Not.Null);
        }

        [Test]
        public void LoadCachedPages_PagesLoaded_PagesAreLoaded()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
            PageNavigationStore.Current.PageTypes["FirstPage"] = typeof(ContentPage);
            PageNavigationStore.Current.PageTypes["SecondPage"] = typeof(ContentPage);
            PageNavigationStore.Current.PageTypes["ThirdPage"] = typeof(ContentPage);
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
            cacheCoordinator.LoadCachedPages("MainPage");
            var pageOne = cacheCoordinator.TryGetCachedPage("FirstPage");
            var pageTwo = cacheCoordinator.TryGetCachedPage("SecondPage");
            var pageThree = cacheCoordinator.TryGetCachedPage("ThirdPage");
            Assert.That(pageOne, Is.Not.Null);
            Assert.That(pageTwo, Is.Not.Null);
            Assert.That(pageThree, Is.Not.Null);
        }

        [Test]
        public void RemoveCachedPages_PagesExist_RemovesAllButSingleInstance()
        {
            var cacheCoordinator = GetPageCacheCoordinator();
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
            var pageOne = cacheCoordinator.TryGetCachedPage("FirstPage");
            var pageTwo = cacheCoordinator.TryGetCachedPage("SecondPage");
            var pageThree = cacheCoordinator.TryGetCachedPage("ThirdPage");
            Assert.That(pageOne, Is.Null);
            Assert.That(pageTwo, Is.Null);
            Assert.That(pageThree, Is.Not.Null);
        }

        public static PageCacheCoordinator GetPageCacheCoordinator()
        {
            StateManager.ResetAll();
            return new PageCacheCoordinator();
        }
    }
}
