using System;
using Atlas.Forms.Pages;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Behaviors
{
    public class TabbedPagePresentationBehavior : Behavior<TabbedPage>
    {
        protected Page LastPage { get; set; }

        protected override void OnAttachedTo(TabbedPage bindable)
        {
            LastPage = bindable.CurrentPage;
            bindable.CurrentPageChanged += OnCurrentPageChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            LastPage = null;
            bindable.CurrentPageChanged -= OnCurrentPageChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnCurrentPageChanged(object sender, EventArgs eventArgs)
        {
            var tabbedPage = (TabbedPage) sender;
            PageActionInvoker.InvokeOnPageDisappeared(LastPage, new ParametersService());
            PageActionInvoker.InvokeOnPageAppeared(tabbedPage.CurrentPage, new ParametersService());
            LastPage = tabbedPage.CurrentPage;
        }
    }
}
