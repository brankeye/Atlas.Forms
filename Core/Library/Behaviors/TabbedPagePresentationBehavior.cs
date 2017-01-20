using System;
using atlas.core.Library.Pages;
using atlas.core.Library.Services;
using Xamarin.Forms;

namespace atlas.core.Library.Behaviors
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
            PageActionInvoker.InvokeOnPageDisappeared(LastPage);
            PageActionInvoker.InvokeOnPageAppeared(tabbedPage.CurrentPage);
            LastPage = tabbedPage.CurrentPage;
        }
    }
}
