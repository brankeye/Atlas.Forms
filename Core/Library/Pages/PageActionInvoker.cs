using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class PageActionInvoker
    {
        public static void InvokeActionOnPage<T>(object view, Action<T> action) where T : class
        {
            var canContinue = true;
            while (canContinue)
            {
                var page = view as Page;
                if (page != null)
                {
                    ActionInvoker.Invoke(page, action);
                    ActionInvoker.Invoke(page.BindingContext, action);
                }

                if (page is NavigationPage)
                {
                    var navigationPage = page as NavigationPage;
                    view = navigationPage.CurrentPage;
                    continue;
                }
                if (page is MasterDetailPage)
                {
                    var masterDetailPage = page as MasterDetailPage;
                    view = masterDetailPage.Detail;
                    continue;
                }
                //if (page is TabbedPage)
                //{
                //    var tabbedPage = page as TabbedPage;
                //    view = tabbedPage.CurrentPage;
                //    continue;
                //}
                //if (page is CarouselPage)
                //{
                //    var carouselPage = page as CarouselPage;
                //    view = carouselPage.CurrentPage;
                //    continue;
                //}

                canContinue = false;
            }
        }

        public static void InvokeOnPageAppearing(object view, IParametersService parameters)
        {
            InvokeActionOnPage<IPageAppearingAware>(view, x => x.OnPageAppearing(parameters));
        }

        public static void InvokeOnPageAppeared(object view, IParametersService parameters)
        {
            InvokeActionOnPage<IPageAppearedAware>(view, x => x.OnPageAppeared(parameters));
        }

        public static void InvokeOnPageDisappearing(object view, IParametersService parameters)
        {
            InvokeActionOnPage<IPageDisappearingAware>(view, x => x.OnPageDisappearing(parameters));
        }

        public static void InvokeOnPageDisappeared(object view, IParametersService parameters)
        {
            InvokeActionOnPage<IPageDisappearedAware>(view, x => x.OnPageDisappeared(parameters));
        }

        public static void InvokeOnPageCaching(object view)
        {
            InvokeActionOnPage<IPageCachingAware>(view, x => x.OnPageCaching());
        }

        public static void InvokeOnPageCached(object view)
        {
            InvokeActionOnPage<IPageCachedAware>(view, x => x.OnPageCached());
        }

        public static void InvokeInitialize(object view, IParametersService parameters)
        {
            InvokeActionOnPage<IInitializeAware>(view, x => x.Initialize(parameters));
        }
    }
}
