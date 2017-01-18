using System;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Interfaces.Pages;
using atlas.core.Library.Utilities;
using Xamarin.Forms;

namespace atlas.core.Library.Pages
{
    public class PageActionInvoker
    {
        public static void InvokeActionOnPage<T>(object view, Action<T> action)
            where T : class
        {
            ActionInvoker.Invoke(view, action);
            ActionInvoker.Invoke((view as Page)?.BindingContext, action);
        }

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
    }
}
