using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Pages.Infos;

namespace Atlas.Forms.Caching
{
    public class PageCacheStore
    {
        public static PageCacheStore Current { get; set; } = new PageCacheStore();

        public IDictionary<string, CacheInfo> PageCache { get; } = new Dictionary<string, CacheInfo>();

        public IReadOnlyDictionary<string, CacheInfo> GetPageCache()
        {
            return new ReadOnlyDictionary<string, CacheInfo>(PageCache);
        }
    }
}
