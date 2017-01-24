using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Caching
{
    public class PageCacheStore
    {
        public static PageCacheStore Current { get; set; } = new PageCacheStore();

        public Dictionary<string, PageCacheContainer> PageCache { get; } = new Dictionary<string, PageCacheContainer>();

        public IReadOnlyDictionary<string, PageCacheContainer> GetPageCache()
        {
            return PageCache;
        }
    }
}
