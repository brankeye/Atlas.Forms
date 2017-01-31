using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Caching
{
    public class PageCacheStore
    {
        public static PageCacheStore Current { get; set; } = new PageCacheStore();

        public IDictionary<string, PageCacheContainer> PageCache { get; } = new Dictionary<string, PageCacheContainer>();

        public IReadOnlyDictionary<string, PageCacheContainer> GetPageCache()
        {
            return new ReadOnlyDictionary<string, PageCacheContainer>(PageCache);
        }
    }
}
