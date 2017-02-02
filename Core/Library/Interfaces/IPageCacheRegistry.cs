using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageCacheRegistry
    {
        IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap { get; }

        ITriggerPageApi WhenPage(string pageKey);

        ITriggerPageApi WhenPage<TPage>() where TPage : Page;

        bool Remove(string pageKey, PageMapContainer container);

        IList<PageMapContainer> GetMappingsForKey(string pageKey);
    }
}
