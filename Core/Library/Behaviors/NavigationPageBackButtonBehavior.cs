using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Behaviors
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

        protected void NavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            var navigationPage = (NavigationPage) sender;
            var currentPage = navigationPage.CurrentPage;
            var previousPage = e.Page;
            PageMethodInvoker.InvokeOnPageDisappearing(previousPage);
            PageMethodInvoker.InvokeOnPageDisappeared(previousPage);
            PageMethodInvoker.InvokeOnPageAppearing(currentPage);
            PageMethodInvoker.InvokeOnPageAppeared(currentPage);
            previousPage.Behaviors?.Clear();
        }
    }
}
