using System;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Behaviors
{
    public class CarouselPagePresentationBehavior : Behavior<CarouselPage>
    {
        protected Page LastPage { get; set; }

        protected IPublisher Publisher { get; set; }

        public CarouselPagePresentationBehavior(IPublisher publisher)
        {
            Publisher = publisher;
        }

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
            Publisher.SendPageDisappearedMessage(LastPage, new ParametersService());
            Publisher.SendPageAppearedMessage(tabbedPage.CurrentPage, new ParametersService());
            LastPage = tabbedPage.CurrentPage;
        }
    }
}
