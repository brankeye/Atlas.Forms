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
        protected IServiceFactoryImp ServiceFactory { get; private set; }

        private IAutoCacheController AutoCacheController { get; set; }

        protected ICacheController CacheController { get; private set; }

        private IPageNavigationStore PageNavigationStore { get; set; }

        private IPageCacheMap PageCacheMap { get; set; }

        private IPageKeyStore PageKeyStore { get; set; }

        protected override void Initialize()
        {
            CreateStores();
            MessagingService.SetCurrent(CreateMessagingService);
            CachePubSubService.SetCurrent(CreateCachePubSubService);
            ServiceFactory = CreateServiceFactory();
            CacheController = CreateCacheController();
            AutoCacheController = CreateAutoCacheController();
            ConfigureServiceFactory();
            base.Initialize();
        }

        protected virtual void ConfigureServiceFactory()
        {
            ServiceFactory.AddNavigationService(CreateNavigationService);
            ServiceFactory.AddNavigationController(CreateNavigationController);
            ServiceFactory.AddPageCacheController(CreatePageRetriever);
            ServiceFactory.Lock();
        }

        protected override INavigationService CreateNavigationService(INavigation navigation)
        {
            var navigationController = CreateNavigationController(navigation);
            var pageCacheController = CreatePageRetriever();
            return new NavigationService(navigationController, pageCacheController, CachePubSubService.Publisher);
        }

        protected override IPageCacheService CreatePageCacheService()
        {
            return new PageCacheService(CreatePageRetriever(), CacheController);
        }

        protected override IPageDialogService CreatePageDialogService()
        {
            return new PageDialogService();
        }

        protected override IPageNavigationRegistry CreatePageNavigationRegistry()
        {
            return new PageNavigationRegistry(PageNavigationStore);
        }

        protected override IPageCacheRegistry CreatePageCacheRegistry()
        {
            return new PageCacheRegistry(PageCacheMap);
        }

        protected virtual INavigationController CreateNavigationController(INavigation navigation)
        {
            var navigationProvider = new NavigationProvider(navigation);
            var pageStackController = CreatePageStackController(navigationProvider);
            return new NavigationController(new ApplicationProvider(), navigationProvider, pageStackController, PageKeyStore);
        }

        protected virtual IPageRetriever CreatePageRetriever()
        {
            return new PageRetriever(CacheController, CreatePageFactory(), CachePubSubService.Publisher);
        }

        protected virtual ICacheController CreateCacheController()
        {
            return new CacheController();
        }

        protected virtual IPageStackController CreatePageStackController(INavigationProvider navigationProvider)
        {
            return new PageStackController(navigationProvider, PageKeyStore);
        }

        protected virtual IServiceFactoryImp CreateServiceFactory()
        {
            return new ServiceFactoryImp();
        }

        protected virtual IAutoCacheController CreateAutoCacheController()
        {
            return new AutoCacheController(CacheController, PageCacheMap, PageKeyStore, CreatePageFactory(), CachePubSubService.Subscriber);
        }

        protected virtual IMessagingService CreateMessagingService()
        {
            return new MessagingService();
        }

        protected virtual ICachePubSubService CreateCachePubSubService()
        {
            return new CachePubSubService(MessagingService.Current);
        }

        protected virtual IPageFactory CreatePageFactory()
        {
            return new PageFactory(PageNavigationStore, PageKeyStore, ServiceFactory);
        }

        protected virtual void CreateStores()
        {
            PageKeyStore = new PageKeyStore();
            PageNavigationStore = new PageNavigationStore();
            PageCacheMap = new PageCacheMap();
        }
    }
}
