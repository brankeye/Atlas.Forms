using System;
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
        protected INavigationController NavigationController { get; set; }

        protected IPageCacheController PageCacheController { get; set; }

        public PageFactory(INavigationController navigationController, IPageCacheController pageCacheController)
        {
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public virtual object GetNewPage(string key)
        {
            Type pageType;
            PageNavigationStore.Current.PageTypes.TryGetValue(key, out pageType);
            var nextPage = Activator.CreateInstance(pageType) as Page;
            TryAddBehaviors(nextPage);
            TryAddManagers(nextPage);
            return nextPage;
        }

        protected virtual void TryAddBehaviors(object page)
        {
            (page as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior());
            (page as TabbedPage)?.Behaviors.Add(new TabbedPagePresentationBehavior());
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
            return new MasterDetailPageManager(page, NavigationController, PageCacheController, this);
        }

        protected virtual ITabbedPageManager GetTabbedPageManager(TabbedPage page)
        {
            return new TabbedPageManager(page, NavigationController, PageCacheController);
        }

        protected virtual ICarouselPageManager GetCarouselPageManager(CarouselPage page)
        {
            return new CarouselPageManager(page, NavigationController, PageCacheController);
        }
    }
}
