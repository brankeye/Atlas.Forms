using System;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Interfaces.Pages;
using atlas.core.Library.Utilities;
using Xamarin.Forms;

namespace atlas.core.Library.Pages
{
    public class PageActionInvoker
    {
        public static void InvokeActionOnPage<T>(object view, Action<T> action) where T : class
        {
            var page = view as T;
            if (page != null)
            {
                ActionInvoker.Invoke(page, action);
                ActionInvoker.Invoke((page as Page)?.BindingContext, action);
            }
        }

        //public static void InvokeActionOnPageForAll<T>(object view, Action<T> action) where T : class
        //{
        //    while (true)
        //    {
        //        if (view is NavigationPage)
        //        {
        //            var innerPage = ((NavigationPage) view).CurrentPage;
        //            view = innerPage;
        //        }
        //        else if (view is MasterDetailPage)
        //        {
        //            var masterDetailPage = (MasterDetailPage) view;
        //            var detailPage = masterDetailPage.Detail;
        //            var masterPage = masterDetailPage.Master;
        //            ActionInvoker.Invoke(masterDetailPage, action);
        //            ActionInvoker.Invoke(masterDetailPage.BindingContext, action);
        //            ActionInvoker.Invoke(masterPage, action);
        //            ActionInvoker.Invoke(masterPage.BindingContext, action);
        //            view = detailPage;
        //        }
        //        else if (view is TabbedPage)
        //        {
        //            var tabbedPage = (TabbedPage) view;
        //            ActionInvoker.Invoke(tabbedPage, action);
        //            ActionInvoker.Invoke(tabbedPage.BindingContext, action);
        //            view = tabbedPage.CurrentPage;
        //        }
        //        else if (view is T)
        //        {
        //            ActionInvoker.Invoke(view, action);
        //            ActionInvoker.Invoke((view as Page)?.BindingContext, action);
        //            break;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //}

        public static void InvokeOnPageAppearing(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageAppearingAware>(view, x => x.OnPageAppearing(parameters));
        }

        public static void InvokeOnPageAppeared(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageAppearedAware>(view, x => x.OnPageAppeared(parameters));
        }

        public static void InvokeOnPageDisappearing(object view)
        {
            InvokeActionOnPage<IPageDisappearingAware>(view, x => x.OnPageDisappearing());
        }

        public static void InvokeOnPageDisappeared(object view)
        {
            InvokeActionOnPage<IPageDisappearedAware>(view, x => x.OnPageDisappeared());
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
