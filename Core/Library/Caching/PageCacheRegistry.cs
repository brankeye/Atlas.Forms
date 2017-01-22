using System;
using atlas.core.Library.Enums;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCacheRegistry : IPageCacheRegistry
    {
        public FluentTriggerPageApi WhenPage(string pageKey)
        {
            var container = new PageMapContainer();
            PageCacheMap.AddPageContainer(pageKey, container);
            return new FluentTriggerPageApi(pageKey, container);
        }

        public FluentTriggerPageApi WhenPage<TPage>() where TPage : Page
        {
            return WhenPage(typeof(TPage).Name);
        }
    }
}
