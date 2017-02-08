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

        private IAutoCacheController AutoCacheController { get; set; }

        protected override void Initialize()
        {
            MessagingService.SetCurrent(CreateMessagingService);
            CachePubSubService.SetCurrent(CreateCachePubSubService);
            AutoCacheController = CreateAutoCacheController();
            ServiceFactory = CreateServiceFactory();
            ConfigureServiceFactory();
            base.Initialize();
        }

        protected virtual void ConfigureServiceFactory()
        {
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
            return new PageCacheService(CreatePageCacheController(), new CacheController());
        }

        protected override IPageDialogService CreatePageDialogService()
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

        protected virtual IPageRetriever CreatePageCacheController()
        {
            return new PageRetriever(new CacheController(), new PageFactory(ServiceFactory));
        }

        protected virtual IPageStackController CreatePageStackController(INavigationProvider navigationProvider)
        {
            return new PageStackController(navigationProvider);
        }

        protected virtual IServiceFactoryImp CreateServiceFactory()
        {
            return new ServiceFactoryImp();
        }

        protected virtual IAutoCacheController CreateAutoCacheController()
        {
            return new AutoCacheController(new CacheController(), new PageFactory(ServiceFactory));
        }

        protected virtual IMessagingService CreateMessagingService()
        {
            return new MessagingService();
        }

        protected virtual ICachePubSubService CreateCachePubSubService()
        {
            return new CachePubSubService();
        }
    }
}
