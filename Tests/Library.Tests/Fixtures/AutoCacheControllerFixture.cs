using System.Collections.Generic;
using NUnit.Framework;
using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Infos;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Library.Tests.Fixtures
{
    [TestFixture]
    public class AutoCacheControllerFixture
    {
        private IPageFactory PageFactory { get; set; }

        private ICacheController CacheController { get; set; }

        [Test]
        public void OnPageAppeared_SaveAppearingPage_SavesPage()
        {
            var autoCacheController = GetAutoCacheControllerForAppears();
            var cachedPage = CacheController.TryGetCacheInfo("FirstPage");
            Assert.That(cachedPage, Is.Null);
            var pageInstance = PageFactory.GetNewPage("FirstPage");
            autoCacheController.OnPageAppeared(pageInstance);
            cachedPage = CacheController.TryGetCacheInfo("FirstPage");
            Assert.That(cachedPage, Is.Not.Null);
            var secondPage = CacheController.TryGetCacheInfo("SecondPage");
            var thirdPage = CacheController.TryGetCacheInfo("ThirdPage");
            Assert.That(secondPage, Is.Not.Null);
            Assert.That(thirdPage, Is.Not.Null);
            autoCacheController.Unsubscribe();
        }

        [Test]
        public void OnPageDisappeared_RemoveCachedPages_RemovesPages()
        {
            var autoCacheController = GetAutoCacheControllerForDisappears();
            var pageInstance = PageFactory.GetNewPage("FirstPage");
            autoCacheController.OnPageAppeared(pageInstance);
            var firstPage = CacheController.TryGetCacheInfo("FirstPage");
            var secondPage = CacheController.TryGetCacheInfo("SecondPage");
            var thirdPage = CacheController.TryGetCacheInfo("ThirdPage");
            Assert.That(firstPage, Is.Not.Null);
            Assert.That(secondPage, Is.Not.Null);
            Assert.That(thirdPage, Is.Not.Null);
            autoCacheController.OnPageDisappeared(firstPage.Page);
            firstPage = CacheController.TryGetCacheInfo("FirstPage");
            secondPage = CacheController.TryGetCacheInfo("SecondPage");
            thirdPage = CacheController.TryGetCacheInfo("ThirdPage");
            Assert.That(firstPage, Is.Null);
            Assert.That(secondPage, Is.Null);
            Assert.That(thirdPage, Is.Null);
            autoCacheController.Unsubscribe();
        }

        [Test]
        public void OnPageCreated_SaveCreatedPage_SavesPage()
        {
            var autoCacheController = GetAutoCacheControllerForCreated();
            var pageInstance = PageFactory.GetNewPage("FirstPage");
            autoCacheController.OnPageCreated(pageInstance);
            var firstPage = CacheController.TryGetCacheInfo("FirstPage");
            var secondPage = CacheController.TryGetCacheInfo("SecondPage");
            var thirdPage = CacheController.TryGetCacheInfo("ThirdPage");
            Assert.That(firstPage, Is.Not.Null);
            Assert.That(secondPage, Is.Not.Null);
            Assert.That(thirdPage, Is.Not.Null);
            autoCacheController.Unsubscribe();
        }

        [Test]
        public void OnPageNavigatedFrom_RemoveKeepAliveInstances_Succeeds()
        {
            var autoCacheController = GetAutoCacheControllerForCreated();
            var pageInstance = PageFactory.GetNewPage("FirstPage");
            autoCacheController.OnPageCreated(pageInstance);
            var firstPage = CacheController.TryGetCacheInfo("FirstPage");
            var secondPage = CacheController.TryGetCacheInfo("SecondPage");
            var thirdPage = CacheController.TryGetCacheInfo("ThirdPage");
            Assert.That(firstPage, Is.Not.Null);
            Assert.That(secondPage, Is.Not.Null);
            Assert.That(thirdPage, Is.Not.Null);
            autoCacheController.OnPageNavigatedFrom(firstPage.Page);
            firstPage = CacheController.TryGetCacheInfo("FirstPage");
            secondPage = CacheController.TryGetCacheInfo("SecondPage");
            thirdPage = CacheController.TryGetCacheInfo("ThirdPage");
            Assert.That(firstPage, Is.Null);
            Assert.That(secondPage, Is.Null);
            Assert.That(thirdPage, Is.Null);
            autoCacheController.Unsubscribe();
        }

        protected IAutoCacheController GetAutoCacheControllerForAppears()
        {
            var pageKeyStore = new PageKeyStore();
            var navigationStore = new PageNavigationStore();
            navigationStore.AddTypeAndConstructorInfo("FirstPage", typeof(ContentPage));
            navigationStore.AddTypeAndConstructorInfo("SecondPage", typeof(ContentPage));
            navigationStore.AddTypeAndConstructorInfo("ThirdPage", typeof(ContentPage));
            PageFactory = new PageFactory(navigationStore, pageKeyStore, new ServiceFactoryImp());
            var pageCacheMap = new PageCacheMap();
            pageCacheMap.AddMapInfos("FirstPage", new List<MapInfo>
            {
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.Appears),
                            new TargetPageInfo("FirstPage", CacheState.KeepAlive)),
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.Appears),
                            new TargetPageInfo("SecondPage", CacheState.KeepAlive)),
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.Appears),
                            new TargetPageInfo("ThirdPage", CacheState.KeepAlive)),
            });
            CacheController = new CacheController();
            return new AutoCacheController(CacheController, pageCacheMap, pageKeyStore, PageFactory, new CachePubSubService(new MessagingService()));
        }

        protected IAutoCacheController GetAutoCacheControllerForDisappears()
        {
            var pageKeyStore = new PageKeyStore();
            var navigationStore = new PageNavigationStore();
            navigationStore.AddTypeAndConstructorInfo("FirstPage", typeof(ContentPage));
            navigationStore.AddTypeAndConstructorInfo("SecondPage", typeof(ContentPage));
            navigationStore.AddTypeAndConstructorInfo("ThirdPage", typeof(ContentPage));
            PageFactory = new PageFactory(navigationStore, pageKeyStore, new ServiceFactoryImp());
            var pageCacheMap = new PageCacheMap();
            pageCacheMap.AddMapInfos("FirstPage", new List<MapInfo>
            {
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.Appears),
                            new TargetPageInfo("FirstPage", CacheState.KeepAlive)),
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.Appears),
                            new TargetPageInfo("SecondPage", CacheState.LifetimeInstance) { LifetimeInstanceKey = "FirstPage" }),
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.Appears),
                            new TargetPageInfo("ThirdPage", CacheState.LifetimeInstance) { LifetimeInstanceKey = "FirstPage" }),
            });
            CacheController = new CacheController();
            return new AutoCacheController(CacheController, pageCacheMap, pageKeyStore, PageFactory, new CachePubSubService(new MessagingService()));
        }

        protected IAutoCacheController GetAutoCacheControllerForCreated()
        {
            var pageKeyStore = new PageKeyStore();
            var navigationStore = new PageNavigationStore();
            navigationStore.AddTypeAndConstructorInfo("FirstPage", typeof(ContentPage));
            navigationStore.AddTypeAndConstructorInfo("SecondPage", typeof(ContentPage));
            navigationStore.AddTypeAndConstructorInfo("ThirdPage", typeof(ContentPage));
            PageFactory = new PageFactory(navigationStore, pageKeyStore, new ServiceFactoryImp());
            var pageCacheMap = new PageCacheMap();
            pageCacheMap.AddMapInfos("FirstPage", new List<MapInfo>
            {
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.IsCreated),
                            new TargetPageInfo("FirstPage", CacheState.KeepAlive)),
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.IsCreated),
                            new TargetPageInfo("SecondPage", CacheState.KeepAlive)),
                new MapInfo(new TriggerPageInfo("FirstPage", TriggerOption.IsCreated),
                            new TargetPageInfo("ThirdPage", CacheState.KeepAlive)),
            });
            CacheController = new CacheController();
            return new AutoCacheController(CacheController, pageCacheMap, pageKeyStore, PageFactory, new CachePubSubService(new MessagingService()));
        }
    }
}
