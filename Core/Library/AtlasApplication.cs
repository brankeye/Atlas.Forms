using Atlas.Forms.Caching;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;
using Atlas.Forms.Services;

namespace Atlas.Forms
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        protected override INavigationService CreateNavigationService()
        {
            return new NavigationService(new ApplicationProvider(), new PageCacheCoordinator());
        }

        protected override IPageCacheService CreatePageCacheService()
        {
            return new PageCacheService(new PageCacheCoordinator());
        }

        protected override IPageNavigationRegistry CreatePageNavigationRegistry()
        {
            return new PageNavigationRegistry();
        }

        protected override IPageCacheRegistry CreatePageCacheRegistry()
        {
            return new PageCacheRegistry();
        }
    }
}
