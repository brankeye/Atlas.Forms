using System.Collections.Generic;
using atlas.core.Library.Enums;
using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    internal class PageCacheStore
    {
        protected static Dictionary<string, PageCacheContainer> CacheStore { get; } = new Dictionary<string, PageCacheContainer>();

        private static readonly object _mutex = new object();

        public static PageCacheContainer TryGetPage(string key)
        {
            lock (_mutex)
            {
                PageCacheContainer page;
                CacheStore.TryGetValue(key, out page);
                return page;
            }
        }

        public static void AddPage(string key, PageCacheContainer page)
        {
            lock (_mutex)
            {
                CacheStore.Add(key, page);
            }
        }

        public static void RemovePages(IList<PageMapContainer> containers)
        {
            lock (_mutex)
            {
                foreach (var container in containers)
                {
                    if (container.CacheState == CacheState.Default)
                    {
                        CacheStore.Remove(container.Key);
                    }
                }
            }
        }

        public static bool RemovePage(string key)
        {
            lock (_mutex)
            {
                return CacheStore.Remove(key);
            }
        }

        public static IReadOnlyDictionary<string, PageCacheContainer> GetCacheStore()
        {
            lock (_mutex)
            {
                return CacheStore;
            }
        }
    }
}
