using System.Collections.Generic;
using System.Linq;
using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Caching
{
    public class PageCacheMap
    {
        protected static Dictionary<string, IList<IPageContainer>> CacheStore { get; } = new Dictionary<string, IList<IPageContainer>>();

        public static IList<IPageContainer> GetCachedPages(string key)
        {
            IList<IPageContainer> list;
            if (CacheStore.TryGetValue(key, out list))
            {
                return list;
            }
            return new List<IPageContainer>();
        }

        public static void AddPageContainer(string key, IPageContainer pageContainer)
        {
            IList<IPageContainer> list;
            if (!CacheStore.TryGetValue(key, out list))
            {
                list = new List<IPageContainer>();
                CacheStore.Add(key, list);
            }
            list.Add(pageContainer);
        }

        public static IReadOnlyDictionary<string, IList<IPageContainer>> GetCacheStore()
        {
            return CacheStore;
        }
    }
}
