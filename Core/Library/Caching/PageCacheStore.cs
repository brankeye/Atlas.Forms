using System.Collections.Generic;
using atlas.core.Library.Pages.Containers;

namespace atlas.core.Library.Caching
{
    internal class PageCacheStore
    {
        public static Dictionary<string, PageCacheContainer> PageCache { get; } = new Dictionary<string, PageCacheContainer>();

        public static IReadOnlyDictionary<string, PageCacheContainer> GetPageCache()
        {
            return PageCache;
        }
    }
}
