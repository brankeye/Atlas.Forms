using System.Collections.Generic;
using System.Linq;

namespace atlas.core.Library.Caching
{
    public class AutoPageCacheStore
    {
        public static Dictionary<string, IList<PageCacheContainer>> CacheStore { get; } = new Dictionary<string, IList<PageCacheContainer>>();

        public static IList<PageCacheContainer> GetCachedPagesForKey(string key)
        {
            IList<PageCacheContainer> pageCacheSet;
            if (CacheStore.TryGetValue(key, out pageCacheSet))
            {
                return pageCacheSet.ToList();
            }
            return new List<PageCacheContainer>();
        }
    }
}
