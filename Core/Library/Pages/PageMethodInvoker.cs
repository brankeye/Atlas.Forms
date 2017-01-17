﻿using System;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Interfaces.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Pages
{
    public static class PageMethodInvoker
    {
        public static void InvokeActionOnPage<T>(object view, Action<T> action)
            where T : class
        {
            var viewAsT = view as T;
            if (viewAsT != null)
            {
                action.Invoke(viewAsT);
            }

            var viewAsPage = view as Page;
            var viewModelAsT = viewAsPage?.BindingContext as T;
            if (viewModelAsT != null)
            {
                action.Invoke(viewModelAsT);
            }
        }

        public static void InvokeOnPageAppearing(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageAppearingAware>(view, x => x.OnPageAppearing(parameters));
        }

        public static void InvokeOnPageAppeared(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageAppearedAware>(view, x => x.OnPageAppeared(parameters));
        }

        public static void InvokeOnPageDisappearing(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageDisappearingAware>(view, x => x.OnPageDisappearing(parameters));
        }

        public static void InvokeOnPageDisappeared(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageDisappearedAware>(view, x => x.OnPageDisappeared(parameters));
        }

        public static void InvokeOnPageCaching(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageCachingAware>(view, x => x.OnPageCaching(parameters));
        }

        public static void InvokeOnPageCached(object view, IParametersService parameters = null)
        {
            InvokeActionOnPage<IPageCachedAware>(view, x => x.OnPageCached(parameters));
        }
    }
}
