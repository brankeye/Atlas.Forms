using System.Collections.Generic;
using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Services
{
    public class PageCacheService : IPageCacheService
    {
        public static IPageCacheService Current { get; internal set; }

        public IReadOnlyDictionary<string, PageCacheContainer> CachedPages => Caching.PageCacheStore.GetCacheStore();

        public IReadOnlyDictionary<string, IList<PageMapContainer>> PageCacheStore => PageCacheMap.GetCacheStore();

        public Page GetPage(string key)
        {
            return Caching.PageCacheStore.TryGetPage(key)?.Page;
        }
    }
}
