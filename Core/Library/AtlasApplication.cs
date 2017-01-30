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

        private IPageFactory PageFactory { get; set; }

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

        protected override IPageService CreatePageService()
        {
            return new PageService(NavigationController, PageCacheController);
        }

        protected override IDialogService CreateDialogService()
        {
            return new DialogService(new ApplicationProvider());
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
            var pageStackController = CreatePageStackController();
            return new NavigationController(new ApplicationProvider(), new NavigationProvider(), pageStackController);
        }

        protected virtual IPageCacheController CreatePageCacheController()
        {
            return new PageCacheController(new CacheController(), NavigationController);
        }

        protected virtual IPageStackController CreatePageStackController()
        {
            return new PageStackController();
        }
    }
}
