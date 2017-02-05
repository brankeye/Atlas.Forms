using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class PagePropertyInjector
    {
        public static void InjectProperty<TSource, TInjectable>(object view, TInjectable injectable, Action<TSource, TInjectable> action)
            where TSource : class
            where TInjectable : class
        {
            var page = view as Page;
            if (page != null)
            {
                PropertyInjector.Inject(page, injectable, action);
                PropertyInjector.Inject(page.BindingContext, injectable, action);
            }
        }

        public static void InjectNavigationService(Page page, INavigationService injectable)
        {
            InjectProperty<INavigationServiceProvider, INavigationService>(page, injectable, (p, inj) => p.NavigationService = inj);
        }

        public static void InjectMasterDetailManager(MasterDetailPage page, IMasterDetailPageManager injectable)
        {
            InjectProperty<IMasterDetailPageProvider, IMasterDetailPageManager>(page, injectable, (p, inj) => p.PageManager = inj);
        }

        public static void InjectTabbedPageManager(TabbedPage page, ITabbedPageManager injectable)
        {
            InjectProperty<ITabbedPageProvider, ITabbedPageManager>(page, injectable, (p, inj) => p.PageManager = inj);
        }

        public static void InjectCarouselPageManager(CarouselPage page, ICarouselPageManager injectable)
        {
            InjectProperty<ICarouselPageProvider, ICarouselPageManager>(page, injectable, (p, inj) => p.PageManager = inj);
        }
    }
}
