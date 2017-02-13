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
    public class PageRetrieverFixture
    {
        [Test]
        public void GetNewPage_PageRegistered_ReturnsPage()
        {
            var pageRetriever = GetPageRetriever();
            var pageInstance = pageRetriever.GetNewPage(Nav.Get("FirstPage").Info());
            Assert.That(pageInstance, Is.Not.Null);
        }

        [Test]
        public void TryGetCachedPage_PageExists_ReturnsPage()
        {
            var pageRetriever = GetPageRetriever();
            var pageInstance = pageRetriever.TryGetCachedPage("ThirdPage", new ParametersService());
            Assert.That(pageInstance, Is.Not.Null);
        }

        [Test]
        public void TryGetCachedOrNewPage_PageExists_ReturnsCachePage()
        {
            var pageRetriever = GetPageRetriever();
            var pageInstance = pageRetriever.TryGetCachedPage("ThirdPage", new ParametersService());
            Assert.That(pageInstance, Is.Not.Null);
        }

        [Test]
        public void GetCachedOrNewPage_PageNotExists_ReturnsNewPage()
        {
            var pageRetriever = GetPageRetriever();
            var pageInstance = pageRetriever.GetCachedOrNewPage(Nav.Get("FirstPage").Info(), new ParametersService());
            Assert.That(pageInstance, Is.Not.Null);
        }

        protected IPageRetriever GetPageRetriever()
        {
            var pageNavigationStore = new PageNavigationStore();
            pageNavigationStore.AddTypeAndConstructorInfo("FirstPage", typeof(ContentPage));
            pageNavigationStore.AddTypeAndConstructorInfo("SecondPage", typeof(ContentPage));
            pageNavigationStore.AddTypeAndConstructorInfo("ThirdPage", typeof(ContentPage));
            var cacheController = new CacheController();
            cacheController.TryAddCacheInfo("ThirdPage",
                new CacheInfo(new ContentPage(), true,
                    new TargetPageInfo("ThirdPage", CacheState.Default)));
            MessagingService.SetCurrent(() => new MessagingService());
            CachePubSubService.SetCurrent(() => new CachePubSubService(MessagingService.Current));
            var pageFactory = new PageFactory(pageNavigationStore, new PageKeyStore(), new ServiceFactoryImp());
            return new PageRetriever(cacheController, pageFactory, CachePubSubService.Publisher);
        }
    }
}
