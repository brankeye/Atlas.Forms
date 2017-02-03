using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Services;

namespace Atlas.Forms
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        private INavigationController NavigationController { get; set; }

        private IPageCacheController PageCacheController { get; set; }

        protected override void Initialize()
        {
            NavigationController = CreateNavigationController();
            PageCacheController = CreatePageCacheController();
            
            base.Initialize();
        }

        protected override INavigationService CreateNavigationService()
        {
            return new NavigationService(NavigationController, PageCacheController);
        }

        protected override IPageCacheService CreatePageService()
        {
            return new PageCacheService(NavigationController, PageCacheController);
        }

        protected override IPageDialogService CreateDialogService()
        {
            return new PageDialogService(new ApplicationProvider());
        }

        protected override IPageNavigationRegistry CreatePageNavigationRegistry()
        {
            return new PageNavigationRegistry();
        }

        protected override IPageCacheRegistry CreatePageCacheRegistry()
        {
            return new PageCacheRegistry();
        }

        protected virtual INavigationController CreateNavigationController()
        {
            var navigationProvider = new NavigationProvider();
            var pageStackController = CreatePageStackController(navigationProvider);
            return new NavigationController(new ApplicationProvider(), navigationProvider, pageStackController);
        }

        protected virtual IPageCacheController CreatePageCacheController()
        {
            return new PageCacheController(new CacheController(), NavigationController);
        }

        protected virtual IPageStackController CreatePageStackController(INavigationProvider navigationProvider)
        {
            return new PageStackController(navigationProvider);
        }
    }
}
