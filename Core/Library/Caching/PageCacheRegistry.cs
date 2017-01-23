using System.Collections.Generic;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCacheRegistry : IPageCacheRegistry
    {
        public ITriggerPageApi WhenPage(string pageKey)
        {
            var container = new PageMapContainer();
            var list = PageCacheMap.Mappings[pageKey];
            if (list == null)
            {
                list = new List<PageMapContainer>();
                PageCacheMap.Mappings[pageKey] = list;
            }
            list.Add(container);
            return new TriggerPageApi(pageKey, container);
        }

        public ITriggerPageApi WhenPage<TPage>() where TPage : Page
        {
            return WhenPage(typeof(TPage).Name);
        }
    }
}
