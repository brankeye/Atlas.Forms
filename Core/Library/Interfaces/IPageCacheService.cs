using System.Collections.Generic;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap { get; }

        IReadOnlyDictionary<string, PageCacheContainer> CachedPages { get; }

        Page GetCachedOrNewPage(string page);

        Page GetCachedPage(string page);

        void AddPage(string key, PageMapContainer container, bool isInitialized = false);
    }
}
