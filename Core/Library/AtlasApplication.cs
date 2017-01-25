using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;
using Atlas.Forms.Services;

namespace Atlas.Forms
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        private IApplicationProvider ApplicationProvider { get; set; }

        private IPageCacheCoordinator CacheCoordinator { get; set; }

        protected override void Initialize()
        {
            ApplicationProvider = CreateApplicationProvider();
            CacheCoordinator = CreatePageCacheCoordinator();
            base.Initialize();
        }

        protected override INavigationService CreateNavigationService()
        {
            return new NavigationService(ApplicationProvider, CacheCoordinator);
        }

        protected override IPageCacheService CreatePageCacheService()
        {
            return new PageCacheService(CacheCoordinator);
        }

        protected override IPageDialogService CreatePageDialogService()
        {
            return new PageDialogService(ApplicationProvider);
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

        protected virtual IPageCacheCoordinator CreatePageCacheCoordinator()
        {
            return new PageCacheCoordinator();
        }
    }
}
