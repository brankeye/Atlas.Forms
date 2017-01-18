using atlas.core.Library;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Services;
using atlas.samples.helloworld.Shared.Views.Pages;
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
        }

        protected override void RegisterPagesForCaching(IPageCacheRegistry registry)
        {
            registry.WhenAppears<Dashboard>().CachePage<Tutorials>().AsKeepAlive();
            registry.WhenAppears<Dashboard>().CachePage<About>().AsKeepAlive();
            registry.WhenAppears<Dashboard>().CachePage<Changelog>().AsKeepAlive();
            registry.WhenAppears<Dashboard>().CachePage<Contact>().AsKeepAlive();

            registry.WhenAppears<Tutorials>().CachePage<TutorialOne>();
            registry.WhenAppears<TutorialOne>().CachePage<TutorialTwo>();
            registry.WhenAppears<TutorialTwo>().CachePage<TutorialThree>();
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
