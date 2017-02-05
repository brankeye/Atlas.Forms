using System.Collections.Generic;
using Atlas.Forms.Pages.Info;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageCacheRegistry
    {
        IReadOnlyDictionary<string, IList<PageMapInfo>> CacheMap { get; }

        ITriggerPageApi WhenPage(string pageKey);

        ITriggerPageApi WhenPage<TPage>() where TPage : Page;

        bool Remove(string pageKey, PageMapInfo info);

        IList<PageMapInfo> GetMappingsForKey(string pageKey);
    }
}
