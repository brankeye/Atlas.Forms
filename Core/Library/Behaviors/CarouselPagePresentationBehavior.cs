using System;
using Atlas.Forms.Pages;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Behaviors
{
    public class CarouselPagePresentationBehavior : Behavior<CarouselPage>
    {
        protected Page LastPage { get; set; }

        protected override void OnAttachedTo(CarouselPage bindable)
        {
            LastPage = bindable.CurrentPage;
            bindable.CurrentPageChanged += OnCurrentPageChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CarouselPage bindable)
        {
            LastPage = null;
            bindable.CurrentPageChanged -= OnCurrentPageChanged;
            base.OnDetachingFrom(bindable);
        }

        protected virtual void OnCurrentPageChanged(object sender, EventArgs eventArgs)
        {
            var tabbedPage = (CarouselPage) sender;
            PageActionInvoker.InvokeOnPageDisappeared(LastPage, new ParametersService());
            PageActionInvoker.InvokeOnPageAppeared(tabbedPage.CurrentPage, new ParametersService());
            LastPage = tabbedPage.CurrentPage;
        }
    }
}
