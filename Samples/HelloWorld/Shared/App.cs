using atlas.samples.helloworld.Shared.Views.Pages;
using atlas.samples.helloworld.Shared.Views.Pages.NavigationPage;
using Atlas.Forms;
using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared
{
    public class App : AtlasApplication
    {
        public App()
        {
            NavigationService.SetMainPage(Nav.Get<MainMasterDetailPage>().Info());
            //NavigationService.SetMainPage(Nav.Get("MyContentPage").AsNavigationPage().Info());
            //NavigationService.PushAsync(Nav.Get("MyNextPage").Info()).Wait();
            //NavigationService.PushAsync(Nav.Get("NextTabPage").Info()).Wait();
            //var pageCache = CacheController.GetPageCache();
            //NavigationService.PopAsync().Wait();
            //pageCache = CacheController.GetPageCache();
            //NavigationService.PopAsync().Wait();
            //pageCache = CacheController.GetPageCache();
        }

        protected override void RegisterPagesForNavigation(IPageNavigationRegistry registry)
        {
            registry.RegisterPage<CustomNavigationPage>("NavigationPage");
            registry.RegisterPage<Views.Pages.MainMasterDetailPage>();
            registry.RegisterPage<Views.Pages.MyContentPage>();
            registry.RegisterPage<Views.Pages.MyTabbedPage>();
            registry.RegisterPage<Views.Pages.FirstTabPage>();
            registry.RegisterPage<Views.Pages.SecondTabPage>();
            registry.RegisterPage<Views.Pages.ThirdTabPage>();
            registry.RegisterPage<Views.Pages.MyNextPage>();
            registry.RegisterPage<Views.Pages.NextTabPage>();
        }

        protected override void RegisterPagesForCaching(IPageCacheRegistry registry)
        {
            registry.WhenPage<MyContentPage>().IsCreated().CachePage().AsLifetimeInstance("MainMasterDetailPage");
            registry.WhenPage<MyTabbedPage>().IsCreated().CachePage().AsLifetimeInstance("MainMasterDetailPage");
            registry.WhenPage<FirstTabPage>().IsCreated().CachePage().AsLifetimeInstance("MyTabbedPage");
            registry.WhenPage<SecondTabPage>().IsCreated().CachePage().AsLifetimeInstance("MyTabbedPage");
            registry.WhenPage<ThirdTabPage>().IsCreated().CachePage().AsLifetimeInstance("MyTabbedPage");

            //registry.WhenPage<MyContentPage>().IsCreated().CachePage();
            //registry.WhenPage<MyContentPage>().IsCreated().CachePage<MyNextPage>();
            //registry.WhenPage<NextTabPage>().IsCreated().CachePage().AsLifetimeInstance<MyNextPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
