using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;
using Xamarin.Forms;

namespace atlas.core.Library.Services
{
    public class PageCacheService : IPageCacheService
    {
        public static IPageCacheService Current { get; internal set; }

        public IReadOnlyDictionary<string, Page> CachedPages => Caching.PageCacheStore.GetCacheStore();

        public IReadOnlyDictionary<string, IList<IPageContainer>> PageCacheStore => PageCacheMap.GetCacheStore();

        public Page GetPage(string key)
        {
            return Caching.PageCacheStore.TryGetPage(key);
        }
    }
}
