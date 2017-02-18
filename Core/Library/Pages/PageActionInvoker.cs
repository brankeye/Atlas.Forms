using System;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class PageActionInvoker : IPageActionInvoker
    {
        protected void InvokeActionOnPage<T>(Page page, Action<T> action) where T : class
        {
            var canContinue = true;
            while (canContinue)
            {
                if (page != null)
                {
                    ActionInvoker.Invoke(page, action);
                    ActionInvoker.Invoke(page.BindingContext, action);
                }

                if (page is NavigationPage)
                {
                    var navigationPage = page as NavigationPage;
                    page = navigationPage.CurrentPage;
                    continue;
                }

                canContinue = false;
            }
        }

        public void InvokeOnPageAppearing(Page page, IParametersService parameters)
        {
            InvokeActionOnPage<IPageAppearingAware>(page, x => x.OnPageAppearing(parameters));
        }

        public void InvokeOnPageAppeared(Page page, IParametersService parameters)
        {
            InvokeActionOnPage<IPageAppearedAware>(page, x => x.OnPageAppeared(parameters));
        }

        public void InvokeOnPageDisappearing(Page page, IParametersService parameters)
        {
            InvokeActionOnPage<IPageDisappearingAware>(page, x => x.OnPageDisappearing(parameters));
        }

        public void InvokeOnPageDisappeared(Page page, IParametersService parameters)
        {
            InvokeActionOnPage<IPageDisappearedAware>(page, x => x.OnPageDisappeared(parameters));
        }

        public void InvokeOnPageCaching(Page page)
        {
            InvokeActionOnPage<IPageCachingAware>(page, x => x.OnPageCaching());
        }

        public void InvokeOnPageCached(Page page)
        {
            InvokeActionOnPage<IPageCachedAware>(page, x => x.OnPageCached());
        }

        public void InvokeInitialize(Page page, IParametersService parameters)
        {
            InvokeActionOnPage<IInitializeAware>(page, x => x.Initialize(parameters));
        }
    }
}
