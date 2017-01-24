using atlas.samples.helloworld.Shared.Views.Pages;
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
            NavigationService.Current.SetMainPage("Dashboard");
        }

        protected override void RegisterPagesForNavigation(IPageNavigationRegistry registry)
        {
            registry.RegisterPage<NavigationPage>();
            registry.RegisterPage<Views.Pages.Dashboard>();
            registry.RegisterPage<Views.Pages.About>();
            registry.RegisterPage<Views.Pages.Changelog>();
            registry.RegisterPage<Views.Pages.Contact>();
            registry.RegisterPage<Views.Pages.Tutorials>();
            registry.RegisterPage<Views.Pages.TutorialOne>();
            registry.RegisterPage<Views.Pages.TutorialTwo>();
            registry.RegisterPage<Views.Pages.TutorialThree>();
            registry.RegisterPage<Views.Pages.TestPage>();
        }

        protected override void RegisterPagesForCaching(IPageCacheRegistry registry)
        {
            registry.WhenPage<Tutorials>().IsCreated().CachePage().AsKeepAlive();
            registry.WhenPage<About>().IsCreated().CachePage().AsKeepAlive();
            registry.WhenPage<Changelog>().IsCreated().CachePage().AsKeepAlive();
            registry.WhenPage<Contact>().IsCreated().CachePage().AsKeepAlive();
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
