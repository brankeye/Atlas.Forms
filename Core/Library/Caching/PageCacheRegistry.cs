using System.Collections.Generic;
using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages;
using Xamarin.Forms;

namespace Atlas.Forms.Caching
{
    public class PageCacheRegistry : IPageCacheRegistry
    {
        public IReadOnlyDictionary<string, IList<MapInfo>> CacheMap => PageCacheMap.GetMappings();

        protected IPageCacheMap PageCacheMap { get; }

        public PageCacheRegistry(IPageCacheMap pageCacheMap)
        {
            PageCacheMap = pageCacheMap;
        }

        public virtual ITriggerPageApi WhenPage(string pageKey)
        {
            var container = new MapInfo();
            var list = PageCacheMap.GetMapInfos(pageKey);
            if (list == null)
            {
                list = new List<MapInfo>();
                PageCacheMap.AddMapInfos(pageKey, list);
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
            var list = PageCacheMap.GetMapInfos(pageKey);
            return list != null && list.Remove(info);
        }

        public virtual IList<MapInfo> GetMappingsForKey(string pageKey)
        {
            var list = PageCacheMap.GetMapInfos(pageKey);
            return list;
        }
    }
}
