using System.Collections.Generic;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Caching
{
    public class PageCacheStore
    {
        public static Dictionary<string, PageCacheContainer> PageCache { get; } = new Dictionary<string, PageCacheContainer>();

        public static IReadOnlyDictionary<string, PageCacheContainer> GetPageCache()
        {
            return PageCache;
        }
    }
}
