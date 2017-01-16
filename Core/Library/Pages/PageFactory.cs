using System;
using atlas.core.Library.Behaviors;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using Xamarin.Forms;

namespace atlas.core.Library.Pages
{
    public class PageFactory : IPageFactory
    {
        public Page CreatePage(string key)
        {
            Page nextPage = null;
            if (PageKeyParser.IsSequence(key))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(key);
                var outerPageType = PageNavigationStore.GetPageType(queue.Dequeue());
                var innerPageType = PageNavigationStore.GetPageType(queue.Dequeue());
                var innerPage = Activator.CreateInstance(innerPageType) as Page;
                nextPage = Activator.CreateInstance(outerPageType, innerPage) as Page;
                (nextPage as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior());
            }
            else
            {
                var pageType = PageNavigationStore.GetPageType(key);
                nextPage = Activator.CreateInstance(pageType) as Page;
            }
            return nextPage;
        }
    }
}
