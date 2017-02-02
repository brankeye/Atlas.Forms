using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using atlas.samples.helloworld.Shared.Views.Pages;
using atlas.samples.helloworld.Shared.Views.Pages.NavigationPages;
using Atlas.Forms;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared
{
    public class App : AtlasApplication
    {
        public App()
        {
            NavigationService.Current.SetMainPage("MainMasterDetailPage");
        }

        protected override void RegisterPagesForNavigation(IPageNavigationRegistry registry)
        {
            registry.RegisterPage<NavigationPage>();
            registry.RegisterPage<Views.Pages.MainMasterDetailPage>();
            registry.RegisterPage<Views.Pages.MyContentPage>();
            registry.RegisterPage<Views.Pages.MyTabbedPage>();
            registry.RegisterPage<Views.Pages.FirstTabPage>();
            registry.RegisterPage<Views.Pages.SecondTabPage>();
            registry.RegisterPage<Views.Pages.ThirdTabPage>();
            registry.RegisterPage<Views.Pages.MyNextPage>();
            registry.RegisterPage<Views.Pages.NextTabPage>();
            registry.RegisterPage<Views.Pages.NavigationPages.MyNavigationContentPage>();
        }

        protected override void RegisterPagesForCaching(IPageCacheRegistry registry)
        {
            registry.WhenPage<MyNavigationContentPage>().IsCreated().CachePage().AsLifetimeInstance<MainMasterDetailPage>();
            registry.WhenPage<MyTabbedPage>().IsCreated().CachePage().AsLifetimeInstance("MainMasterDetailPage");
            registry.WhenPage<FirstTabPage>().IsCreated().CachePage().AsLifetimeInstance("MyTabbedPage");
            registry.WhenPage<SecondTabPage>().IsCreated().CachePage().AsLifetimeInstance("MyTabbedPage");
            registry.WhenPage<ThirdTabPage>().IsCreated().CachePage().AsLifetimeInstance("MyTabbedPage");
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
