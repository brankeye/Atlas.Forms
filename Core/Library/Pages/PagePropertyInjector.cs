using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
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

        public static void InjectMasterDetailManager(MasterDetailPage page, IMasterDetailPageManager injectable)
        {
            InjectProperty<IMasterDetailPageProvider, IMasterDetailPageManager>(page, injectable, (p, inj) => p.Manager = inj);
        }

        public static void InjectTabbedPageManager(TabbedPage page, ITabbedPageManager injectable)
        {
            InjectProperty<ITabbedPageProvider, ITabbedPageManager>(page, injectable, (p, inj) => p.Manager = inj);
        }

        public static void InjectCarouselPageManager(CarouselPage page, ICarouselPageManager injectable)
        {
            InjectProperty<ICarouselPageProvider, ICarouselPageManager>(page, injectable, (p, inj) => p.Manager = inj);
        }
    }
}
