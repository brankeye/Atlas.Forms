using System.Reflection;
using Atlas.Forms.Behaviors;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Pages;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class PageFactory : IPageFactory
    {
        protected IPageNavigationStore PageNavigationStore { get; }

        protected IPageKeyStore PageKeyStore { get; }

        protected IServiceFactoryImp ServiceFactory { get; }

        public PageFactory(IPageNavigationStore pageNavigationStore, 
                           IPageKeyStore pageKeyStore, 
                           IServiceFactoryImp serviceFactory)
        {
            PageNavigationStore = pageNavigationStore;
            PageKeyStore = pageKeyStore;
            ServiceFactory = serviceFactory;
        }

        public virtual Page GetNewPage(string key, Page pageArg = null)
        {
            ConstructorInfo constructor = PageNavigationStore.GetConstructor(key);
            object[] parameters;
            if (pageArg != null)
            {
                parameters = new[] { pageArg };
            }
            else
            {
                parameters = new object[] {};
            }
            var nextPage = constructor?.Invoke(parameters) as Page;
            PageKeyStore.AddPageKey(nextPage, key);
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
            (page as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior(ServiceFactory.CreatePublisher()));
            (page as TabbedPage)?.Behaviors.Add(new TabbedPagePresentationBehavior(ServiceFactory.CreatePublisher()));
            (page as CarouselPage)?.Behaviors.Add(new CarouselPagePresentationBehavior(ServiceFactory.CreatePublisher()));
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
            return new MasterDetailPageManager(page, ServiceFactory.CreatePageCacheController(), ServiceFactory.CreatePublisher());
        }

        protected virtual ITabbedPageManager GetTabbedPageManager(TabbedPage page)
        {
            return new TabbedPageManager(page, ServiceFactory.CreatePageCacheController(), PageKeyStore);
        }

        protected virtual ICarouselPageManager GetCarouselPageManager(CarouselPage page)
        {
            return new CarouselPageManager(page, ServiceFactory.CreatePageCacheController(), PageKeyStore);
        }
    }
}
