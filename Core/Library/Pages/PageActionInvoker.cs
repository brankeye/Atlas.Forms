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
