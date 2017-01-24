using System.Collections.Generic;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Caching
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

        public bool Remove(string pageKey, PageMapContainer container)
        {
            var list = PageCacheMap.Mappings[pageKey];
            return list.Remove(container);
        }

        public IList<PageMapContainer> GetMappingsForKey(string pageKey)
        {
            return PageCacheMap.Mappings[pageKey];
        }
    }
}
