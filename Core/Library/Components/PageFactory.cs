using System.Reflection;
using Atlas.Forms.Behaviors;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageFactory : IPageFactory
    {
        protected IServiceFactoryImp ServiceFactory { get; set; }

        public PageFactory(IServiceFactoryImp serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public virtual object GetNewPage(string key, object pageArg = null)
        {
            ConstructorInfo constructor;
            PageNavigationStore.Current.PageConstructors.TryGetValue(key, out constructor);
            object[] parameters;
            if (pageArg != null)
            {
                parameters = new[] { pageArg };
            }
            else
            {
                parameters = new object[] {};
            }
            var nextPage = constructor?.Invoke(parameters);
            var page = nextPage as Page;
            PageKeyStore.Current.PageKeys.Add(page, key);
            TryAddServices(nextPage);
            TryAddBehaviors(nextPage);
            TryAddManagers(nextPage);
            return nextPage;
        }

        protected virtual void TryAddServices(object page)
        {
            var pageInstance = page as Page;
            if (pageInstance != null)
            {
                var navigationService = ServiceFactory.CreateNavigationService(pageInstance.Navigation);
                PagePropertyInjector.InjectNavigationService(pageInstance, navigationService);
            }
        }

        protected virtual void TryAddBehaviors(object page)
        {
            (page as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior());
            (page as TabbedPage)?.Behaviors.Add(new TabbedPagePresentationBehavior());
            (page as CarouselPage)?.Behaviors.Add(new CarouselPagePresentationBehavior());
        }

        protected virtual void TryAddManagers(object page)
        {
            var masterDetailPage = page as MasterDetailPage;
            if (masterDetailPage != null)
            {
                var manager = GetMasterDetailPageManager(masterDetailPage);
                PagePropertyInjector.InjectMasterDetailManager(masterDetailPage, manager);
                return;
            }

            var tabbedPage = page as TabbedPage;
            if (tabbedPage != null)
            {
                var manager = GetTabbedPageManager(tabbedPage);
                PagePropertyInjector.InjectTabbedPageManager(tabbedPage, manager);
                return;
            }

            var carouselPage = page as CarouselPage;
            if (carouselPage != null)
            {
                var manager = GetCarouselPageManager(carouselPage);
                PagePropertyInjector.InjectCarouselPageManager(carouselPage, manager);
            }
        }

        protected virtual IMasterDetailPageManager GetMasterDetailPageManager(MasterDetailPage page)
        {
            return new MasterDetailPageManager(page, ServiceFactory.CreateNavigationController(page.Navigation), ServiceFactory.CreatePageCacheController());
        }

        protected virtual ITabbedPageManager GetTabbedPageManager(TabbedPage page)
        {
            return new TabbedPageManager(page, ServiceFactory.CreateNavigationController(page.Navigation), ServiceFactory.CreatePageCacheController());
        }

        protected virtual ICarouselPageManager GetCarouselPageManager(CarouselPage page)
        {
            return new CarouselPageManager(page, ServiceFactory.CreateNavigationController(page.Navigation), ServiceFactory.CreatePageCacheController());
        }
    }
}
