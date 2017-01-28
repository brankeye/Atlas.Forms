using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Services;

namespace Atlas.Forms
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        private IApplicationProvider ApplicationProvider { get; set; }

        private INavigationProvider NavigationProvider { get; set; }

        private IPageCacheCoordinator CacheCoordinator { get; set; }

        private IPageStackController PageStackController { get; set; }

        protected override void Initialize()
        {
            PageStackController = CreatePageStackController();
            ApplicationProvider = CreateApplicationProvider();
            NavigationProvider = CreateNavigationProvider();
            CacheCoordinator = CreatePageCacheCoordinator();
            base.Initialize();
        }

        protected override INavigationService CreateNavigationService()
        {
            return new NavigationService(ApplicationProvider, NavigationProvider, CacheCoordinator, PageStackController);
        }

        protected override IPageService CreatePageService()
        {
            return new PageService(CacheCoordinator, NavigationProvider);
        }

        protected override IDialogService CreateDialogService()
        {
            return new DialogService(ApplicationProvider);
        }

        protected override IPageNavigationRegistry CreatePageNavigationRegistry()
        {
            return new PageNavigationRegistry();
        }

        protected override IPageCacheRegistry CreatePageCacheRegistry()
        {
            return new PageCacheRegistry();
        }

        protected virtual IApplicationProvider CreateApplicationProvider()
        {
            return new ApplicationProvider();
        }

        protected virtual INavigationProvider CreateNavigationProvider()
        {
            return new NavigationProvider();
        }

        protected virtual IPageProcessor CreatePageProcessor()
        {
            return new PageProcessor(NavigationProvider, PageStackController);
        }

        protected virtual IPageCacheCoordinator CreatePageCacheCoordinator()
        {
            return new PageCacheCoordinator(CreatePageProcessor());
        }

        protected virtual IPageStackController CreatePageStackController()
        {
            return new PageStackController();
        }
    }
}
