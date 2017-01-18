using System.Collections.Generic;
using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;

namespace atlas.core.Library.Caching
{
    public class PageCacheMap
    {
        protected static Dictionary<string, IList<PageMapContainer>> CacheStore { get; } = new Dictionary<string, IList<PageMapContainer>>();

        public static IList<PageMapContainer> GetCachedPages(string key)
        {
            IList<PageMapContainer> list;
            if (CacheStore.TryGetValue(key, out list))
            {
                return list;
            }
            return new List<PageMapContainer>();
        }

        public static void AddPageContainer(string key, PageMapContainer pageContainer)
        {
            IList<PageMapContainer> list;
            if (!CacheStore.TryGetValue(key, out list))
            {
                list = new List<PageMapContainer>();
                CacheStore.Add(key, list);
            }
            list.Add(pageContainer);
        }

        public static IReadOnlyDictionary<string, IList<PageMapContainer>> GetCacheStore()
        {
            return CacheStore;
        }
    }
}
