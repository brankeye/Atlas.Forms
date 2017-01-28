using Atlas.Forms.Behaviors;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Navigation;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class PageProcessor : IPageProcessor
    {
        protected INavigationProvider NavigationProvider { get; }

        protected IPageStackController PageStackController { get; }

        public PageProcessor(INavigationProvider navigationProvider, IPageStackController pageStackController)
        {
            NavigationProvider = navigationProvider;
            PageStackController = pageStackController;
        }

        public void AddBehaviors(Page page)
        {
            (page as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior());
            (page as TabbedPage)?.Behaviors.Add(new TabbedPagePresentationBehavior());
        }

        public void AddManagers(Page page, IPageCacheCoordinator cacheCoordinator)
        {
            var masterDetailPage = page as MasterDetailPage;
            if (masterDetailPage != null)
            {
                var manager = GetMasterDetailPageManager(masterDetailPage, cacheCoordinator);
                PagePropertyInjector.InjectMasterDetailManager(masterDetailPage, manager);
                return;
            }

            var tabbedPage = page as TabbedPage;
            if (tabbedPage != null)
            {
                var manager = GetTabbedPageManager(tabbedPage, cacheCoordinator);
                PagePropertyInjector.InjectTabbedPageManager(tabbedPage, manager);
                return;
            }

            var carouselPage = page as CarouselPage;
            if (carouselPage != null)
            {
                var manager = GetCarouselPageManager(carouselPage, cacheCoordinator);
                PagePropertyInjector.InjectCarouselPageManager(carouselPage, manager);
            }
        }

        protected virtual IMasterDetailPageManager GetMasterDetailPageManager(MasterDetailPage page, IPageCacheCoordinator cacheCoordinator)
        {
            return new MasterDetailPageManager(page, NavigationProvider, cacheCoordinator, PageStackController);
        }

        protected virtual ITabbedPageManager GetTabbedPageManager(TabbedPage page, IPageCacheCoordinator cacheCoordinator)
        {
            return new TabbedPageManager(page, NavigationProvider, cacheCoordinator, PageStackController);
        }

        protected virtual ICarouselPageManager GetCarouselPageManager(CarouselPage page, IPageCacheCoordinator cacheCoordinator)
        {
            return new CarouselPageManager(page, NavigationProvider, cacheCoordinator, PageStackController);
        }
    }
}
