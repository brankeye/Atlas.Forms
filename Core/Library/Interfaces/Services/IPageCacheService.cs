using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Services
{
    public interface IPageCacheService
    {
        IReadOnlyDictionary<string, IList<PageMapContainer>> CacheMap { get; }

        IReadOnlyDictionary<string, PageCacheContainer> CachedPages { get; }

        Page GetCachedOrNewPage(string page, IParametersService parameters = null);

        Page TryGetCachedPage(string page, IParametersService parameters = null);

        bool TryAddPage(string key);

        bool TryAddPageAsKeepAlive(string key);

        bool TryAddPageAsSingleInstance(string key);

        bool RemovePageFromCache(string key);
    }
}
