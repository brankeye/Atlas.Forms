using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap { get; }

        IReadOnlyDictionary<string, PageCacheContainer> CachedPages { get; }

        Page GetCachedOrNewPage(string page, IParametersService parameters = null);

        Page GetCachedPage(string page, IParametersService parameters = null);

        void AddPage(string key, PageMapContainer container, bool isInitialized = false);
    }
}
