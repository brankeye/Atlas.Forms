using System.Collections.Generic;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Infos;
using Xamarin.Forms;

namespace Atlas.Forms.Caching
{
    public class PageCacheRegistry : IPageCacheRegistry
    {
        public virtual IReadOnlyDictionary<string, IList<MapInfo>> CacheMap => PageCacheMap.Current.GetMappings();

        public virtual ITriggerPageApi WhenPage(string pageKey)
        {
            var container = new MapInfo();
            IList<MapInfo> list;
            PageCacheMap.Current.Mappings.TryGetValue(pageKey, out list);
            if (list == null)
            {
                list = new List<MapInfo>();
                PageCacheMap.Current.Mappings[pageKey] = list;
            }
            list.Add(container);
            return new TriggerPageApi(pageKey, container);
        }

        public virtual ITriggerPageApi WhenPage<TPage>() where TPage : Page
        {
            return WhenPage(typeof(TPage).Name);
        }

        public virtual bool Remove(string pageKey, MapInfo info)
        {
            IList<MapInfo> list;
            PageCacheMap.Current.Mappings.TryGetValue(pageKey, out list);
            return list != null && list.Remove(info);
        }

        public virtual IList<MapInfo> GetMappingsForKey(string pageKey)
        {
            IList<MapInfo> list;
            PageCacheMap.Current.Mappings.TryGetValue(pageKey, out list);
            return list;
        }
    }
}
