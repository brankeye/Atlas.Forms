using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Behaviors
{
    public class NavigationPageBackButtonBehavior : Behavior<NavigationPage>
    {
        protected IPublisher Publisher { get; set; }

        public NavigationPageBackButtonBehavior(IPublisher publisher)
        {
            Publisher = publisher;
        }

        protected override void OnAttachedTo(NavigationPage bindable)
        {
            bindable.Popped += NavigationPage_Popped;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(NavigationPage bindable)
        {
            bindable.Popped -= NavigationPage_Popped;
            base.OnDetachingFrom(bindable);
        }

        protected virtual void NavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            var navigationPage = (NavigationPage) sender;
            var currentPage = navigationPage.CurrentPage;
            var previousPage = e.Page;
            Publisher.SendPageDisappearedMessage(previousPage, new ParametersService());
            Publisher.SendPageAppearedMessage(currentPage, new ParametersService());
        }
    }
}
