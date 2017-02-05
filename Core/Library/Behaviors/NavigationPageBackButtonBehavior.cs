using Atlas.Forms.Pages;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Behaviors
{
    public class NavigationPageBackButtonBehavior : Behavior<NavigationPage>
    {
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
            PageActionInvoker.InvokeOnPageDisappeared(previousPage, new ParametersService());
            PageActionInvoker.InvokeOnPageAppeared(currentPage, new ParametersService());
            previousPage.Behaviors?.Clear();
        }
    }
}
