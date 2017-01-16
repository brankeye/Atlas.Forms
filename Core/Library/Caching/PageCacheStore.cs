using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Navigation;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCacheStore
    {
        protected static Dictionary<string, Page> CacheStore { get; } = new Dictionary<string, Page>();

        public static Page TryGetPage(string key)
        {
            Page page;
            CacheStore.TryGetValue(key, out page);
            return page;
        }

        public static void AddPage(string key, Page page)
        {
            CacheStore.Add(key, page);
        }

        public static void RemovePages(IList<IPageContainer> containers)
        {
            foreach (var container in containers)
            {
                RemovePage(container.Key);
            }
        }

        public static bool RemovePage(string key)
        {
            return CacheStore.Remove(key);
        }

        public static IReadOnlyDictionary<string, Page> GetCacheStore()
        {
            return CacheStore;
        }
    }
}
