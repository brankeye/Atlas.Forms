using Atlas.Forms.Caching;
using Atlas.Forms.Components;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms
{
    public abstract class AtlasApplication : AtlasApplicationBase
    {
        protected INavigationService NavigationService { get; set; }

        protected override void Initialize()
        {
            base.Initialize();
            ServiceFactoryImp.Current.AddService(typeof(INavigationService), args => CreateNavigationService(args[0] as INavigation));
            ServiceFactoryImp.Current.AddService(typeof(INavigationController), args => CreateNavigationController(args[0] as INavigation));
            ServiceFactoryImp.Current.AddService(typeof(IPageCacheController), args => CreatePageCacheController());
            NavigationService = CreateNavigationService();
        }

        protected override INavigationService CreateNavigationService(INavigation navigation = null)
        {
            var navigationController = CreateNavigationController(navigation);
            var pageCacheController = CreatePageCacheController();
            return new NavigationService(navigationController, pageCacheController);
        }

        protected override IPageCacheService CreatePageService()
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

        protected virtual INavigationController CreateNavigationController(INavigation navigation = null)
        {
            var navigationProvider = new NavigationProvider(navigation);
            var pageStackController = CreatePageStackController(navigationProvider);
            return new NavigationController(new ApplicationProvider(), navigationProvider, pageStackController);
        }

        protected virtual IPageCacheController CreatePageCacheController()
        {
            return new PageCacheController(new CacheController(), new PageFactory(ServiceFactoryImp.Current));
        }

        protected virtual IPageStackController CreatePageStackController(INavigationProvider navigationProvider)
        {
            return new PageStackController(navigationProvider);
        }
    }
}
