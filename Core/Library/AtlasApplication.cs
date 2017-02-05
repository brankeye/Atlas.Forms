using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        protected IServiceFactoryImp ServiceFactory { get; set; }

        protected override void Initialize()
        {
            base.Initialize();
            ConfigureServiceFactory();
        }

        protected virtual void ConfigureServiceFactory()
        {
            ServiceFactory = CreateServiceFactory();
            ServiceFactory.AddNavigationService(CreateNavigationService);
            ServiceFactory.AddNavigationController(CreateNavigationController);
            ServiceFactory.AddPageCacheController(CreatePageCacheController);
            ServiceFactory.Lock();
        }

        protected override INavigationService CreateNavigationService(INavigation navigation)
        {
            var navigationController = CreateNavigationController(navigation);
            var pageCacheController = CreatePageCacheController();
            return new NavigationService(navigationController, pageCacheController);
        }

        protected override IPageCacheService CreatePageCacheService()
        {
            return new PageCacheService(CreatePageCacheController());
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

        protected virtual INavigationController CreateNavigationController(INavigation navigation)
        {
            var navigationProvider = new NavigationProvider(navigation);
            var pageStackController = CreatePageStackController(navigationProvider);
            return new NavigationController(new ApplicationProvider(), navigationProvider, pageStackController);
        }

        protected virtual IPageCacheController CreatePageCacheController()
        {
            return new PageCacheController(new CacheController(), new PageFactory(ServiceFactory));
        }

        protected virtual IPageStackController CreatePageStackController(INavigationProvider navigationProvider)
        {
            return new PageStackController(navigationProvider);
        }

        protected virtual IServiceFactoryImp CreateServiceFactory()
        {
            return new ServiceFactoryImp();
        }
    }
}
