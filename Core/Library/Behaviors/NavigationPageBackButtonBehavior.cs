﻿using atlas.core.Library.Pages;
using atlas.core.Library.Services;
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
            PageActionInvoker.InvokeOnPageDisappearing(previousPage);
            PageActionInvoker.InvokeOnPageDisappeared(previousPage);
            PageActionInvoker.InvokeOnPageAppearing(currentPage, new ParametersService());
            PageActionInvoker.InvokeOnPageAppeared(currentPage, new ParametersService());
            previousPage.Behaviors?.Clear();
        }
    }
}
