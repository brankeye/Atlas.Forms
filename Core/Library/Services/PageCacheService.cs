using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.core.Library.Caching;
using atlas.core.Library.Interfaces;
using Xamarin.Forms;

namespace atlas.core.Library.Services
{
    public class PageCacheService : IPageCacheService
    {
        public IReadOnlyDictionary<string, Page> CachedPages => Caching.PageCacheStore.CacheStore;

        public IReadOnlyDictionary<string, IList<PageCacheContainer>> PageCacheStore => AutoPageCacheStore.CacheStore;

        public Page GetPage(string page)
        {
            return Caching.PageCacheStore.GetCachedPage(page);
        }
    }
}
