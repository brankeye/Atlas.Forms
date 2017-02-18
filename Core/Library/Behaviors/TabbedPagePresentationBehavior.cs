using System;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Behaviors
{
    public class TabbedPagePresentationBehavior : Behavior<TabbedPage>
    {
        protected Page LastPage { get; set; }

        protected IPublisher Publisher { get; set; }

        public TabbedPagePresentationBehavior(IPublisher publisher)
        {
            Publisher = publisher;
        }

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

        protected virtual void OnCurrentPageChanged(object sender, EventArgs eventArgs)
        {
            var tabbedPage = (TabbedPage) sender;
            Publisher.SendPageDisappearedMessage(LastPage, new ParametersService());
            Publisher.SendPageAppearedMessage(tabbedPage.CurrentPage, new ParametersService());
            LastPage = tabbedPage.CurrentPage;
        }
    }
}
