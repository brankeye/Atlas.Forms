using System.Collections.Generic;
using atlas.core.Library.Enums;
using atlas.core.Library.Pages.Containers;

namespace atlas.core.Library.Caching
{
    internal class PageCacheStore
    {
        protected static Dictionary<string, PageCacheContainer> CacheStore { get; } = new Dictionary<string, PageCacheContainer>();

        public static PageCacheContainer TryGetPage(string key)
        {
            PageCacheContainer page;
            CacheStore.TryGetValue(key, out page);
            return page;
        }

        public static void AddPage(string key, PageCacheContainer page)
        {
            CacheStore.Add(key, page);
        }

        public static void RemovePages(IList<PageMapContainer> containers)
        {
            foreach (var container in containers)
            {
                if (container.CacheState == CacheState.Default)
                {
                    CacheStore.Remove(container.Key);
                }
            }
        }

        public static bool RemovePage(string key)
        {
            return CacheStore.Remove(key);
        }

        public static IReadOnlyDictionary<string, PageCacheContainer> GetCacheStore()
        {
            return CacheStore;
        }
    }
}
