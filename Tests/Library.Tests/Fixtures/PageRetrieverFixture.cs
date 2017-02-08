using System.Collections.Generic;
using NUnit.Framework;
using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages.Infos;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Library.Tests.Fixtures
{
    [TestFixture]
    public class PageRetrieverFixture
    {
        //[Test]
        //public void GetCachedOrNewPage_PageNotRegistered_ReturnsNull()
        //{
        //    var cacheCoordinator = GetPageCacheController();
        //    var page = cacheCoordinator.TryGetCachedPage("MainPage", new ParametersService());
        //    Assert.That(page, Is.Null);
        //}

        //[Test]
        //public void GetCachedOrNewPage_PageRegistered_ReturnsPage()
        //{
        //    var cacheCoordinator = GetPageCacheController();
        //    PageNavigationStore.Current.AddTypeAndConstructorInfo("MainPage", typeof(ContentPage));
        //    var page = cacheCoordinator.TryGetCachedPage("MainPage", new ParametersService());
        //    Assert.That(page, Is.Null);
        //}

        //[Test]
        //public void GetCachedOrNewPage_PageExists_ReturnsPage()
        //{
        //    var cacheCoordinator = GetPageCacheController();
        //    PageCacheStore.Current.PageCache["MainPage"] = new CacheInfo
        //    {
        //        Key = "MainPage",
        //        Page = new ContentPage()
        //    };
        //    var page = cacheCoordinator.TryGetCachedPage("MainPage", new ParametersService());
        //    Assert.That(page, Is.Not.Null);
        //}

        //[Test]
        //public void AddCachedPages_PagesLoaded_PagesAreLoaded()
        //{
        //    var cacheCoordinator = GetPageCacheController();
        //    PageNavigationStore.Current.AddTypeAndConstructorInfo("FirstPage", typeof(ContentPage));
        //    PageNavigationStore.Current.AddTypeAndConstructorInfo("SecondPage", typeof(ContentPage));
        //    PageNavigationStore.Current.AddTypeAndConstructorInfo("ThirdPage", typeof(ContentPage));
        //    var list = new List<MapInfo>
        //    {
        //        new MapInfo
        //        {
        //            Key = "FirstPage",
        //            Type = typeof(ContentPage),
        //            CacheOption = CacheOption.Appears,
        //            CacheState = CacheState.Default
        //        },
        //        new MapInfo
        //        {
        //            Key = "SecondPage",
        //            Type = typeof(ContentPage),
        //            CacheOption = CacheOption.Appears,
        //            CacheState = CacheState.KeepAlive
        //        },
        //        new MapInfo
        //        {
        //            Key = "ThirdPage",
        //            Type = typeof(ContentPage),
        //            CacheOption = CacheOption.Appears,
        //            CacheState = CacheState.SingleInstance
        //        }
        //    };
        //    PageCacheMap.Current.Mappings["MainPage"] = list;
        //    //cacheCoordinator.AddCachedPages("MainPage");
        //    CacheInfo one, two, three;
        //    PageCacheStore.Current.PageCache.TryGetValue("FirstPage", out one);
        //    PageCacheStore.Current.PageCache.TryGetValue("SecondPage", out two);
        //    PageCacheStore.Current.PageCache.TryGetValue("ThirdPage", out three);
        //    Assert.That(one, Is.Not.Null);
        //    Assert.That(two, Is.Not.Null);
        //    Assert.That(three, Is.Not.Null);
        //}

        //[Test]
        //public void RemoveCachedPages_PagesExist_RemovesAllButSingleInstance()
        //{
        //    var cacheCoordinator = GetPageCacheController();
        //    var list = new List<MapInfo>
        //    {
        //        new MapInfo
        //        {
        //            Key = "FirstPage",
        //            Type = typeof(ContentPage),
        //            CacheOption = CacheOption.Appears,
        //            CacheState = CacheState.Default
        //        },
        //        new MapInfo
        //        {
        //            Key = "SecondPage",
        //            Type = typeof(ContentPage),
        //            CacheOption = CacheOption.Appears,
        //            CacheState = CacheState.KeepAlive
        //        },
        //        new MapInfo
        //        {
        //            Key = "ThirdPage",
        //            Type = typeof(ContentPage),
        //            CacheOption = CacheOption.Appears,
        //            CacheState = CacheState.SingleInstance
        //        }
        //    };
        //    PageCacheMap.Current.Mappings["MainPage"] = list;
        //    PageCacheStore.Current.PageCache["FirstPage"] = new CacheInfo
        //    {
        //        Page = new ContentPage(),
        //        Type = typeof(ContentPage),
        //        CacheOption = CacheOption.Appears,
        //        CacheState = CacheState.Default
        //    };
        //    PageCacheStore.Current.PageCache["SecondPage"] = new CacheInfo
        //    {
        //        Page = new ContentPage(),
        //        Type = typeof(ContentPage),
        //        CacheOption = CacheOption.Appears,
        //        CacheState = CacheState.KeepAlive
        //    };
        //    PageCacheStore.Current.PageCache["ThirdPage"] = new CacheInfo
        //    {
        //        Page = new ContentPage(),
        //        Type = typeof(ContentPage),
        //        CacheOption = CacheOption.Appears,
        //        CacheState = CacheState.SingleInstance
        //    };
        //    //cacheCoordinator.RemoveCachedPages("MainPage");
        //    CacheInfo one, two, three;
        //    PageCacheStore.Current.PageCache.TryGetValue("FirstPage", out one);
        //    PageCacheStore.Current.PageCache.TryGetValue("SecondPage", out two);
        //    PageCacheStore.Current.PageCache.TryGetValue("ThirdPage", out three);
        //    Assert.That(one, Is.Null);
        //    Assert.That(two, Is.Null);
        //    Assert.That(three, Is.Not.Null);
        //}

        public static IPageRetriever GetPageCacheController()
        {
            return new PageRetriever(new CacheController(), new PageFactory(new PageNavigationStore(), new PageKeyStore(), new ServiceFactoryImp()), CachePubSubService.Publisher);
        }
    }
}
