using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using atlas.core.Library.Pages;
using atlas.core.Library.Services;

namespace atlas.core.Library
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        protected override void CreateNavigationService()
        {
            // initializes static Current instance of NavigationService
            var navigationService = new NavigationService(new ApplicationProvider(), new PageFactory());
        }

        protected override IPageNavigationRegistry CreatePageNavigationRegistry()
        {
            return new PageNavigationRegistry();
        }

        protected override IPageCacheRegistry CreatePageCacheRegistry()
        {
            return new AutoPageCacheRegistry();
        }
    }
}
