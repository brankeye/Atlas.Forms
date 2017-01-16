﻿using atlas.core.Library;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared
{
    public class App : AtlasApplication
    {
        public App()
        {
            // The root page of your application
            var cacheService = new PageCacheService();
            var pageCacheStore = cacheService.PageCacheStore;
            var cachedPages = cacheService.CachedPages;
            NavigationService.Current.SetMainPage("NavigationPage/Dashboard");
            cachedPages = cacheService.CachedPages;
        }

        protected override void RegisterPagesForNavigation(IPageNavigationRegistry registry)
        {
            registry.RegisterPage<NavigationPage>();
            registry.RegisterPage<Views.Pages.Dashboard>();
            registry.RegisterPage<Views.Pages.About>();
            registry.RegisterPage<Views.Pages.Changelog>();
            registry.RegisterPage<Views.Pages.Contact>();
        }

        protected override void RegisterPagesForCaching(IPageCacheRegistry registry)
        {
            registry.RegisterPageForCache<Views.Pages.Dashboard, Views.Pages.About>();
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
