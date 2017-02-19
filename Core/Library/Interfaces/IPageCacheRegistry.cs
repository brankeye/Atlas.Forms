using System.Collections.Generic;
using Atlas.Forms.Infos;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageCacheRegistry
    {
        IReadOnlyDictionary<string, IList<MapInfo>> CacheMap { get; }

        ITriggerPageApi WhenPage(string pageKey);

        ITriggerPageApi WhenPage<TPage>() where TPage : Page;

        bool Remove(string pageKey, MapInfo info);

        IList<MapInfo> GetMappingsForKey(string pageKey);
    }
}
