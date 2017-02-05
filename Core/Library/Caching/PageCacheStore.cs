using System.Collections.Generic;
using System.Collections.ObjectModel;
using Atlas.Forms.Pages.Info;

namespace Atlas.Forms.Caching
{
    public class PageCacheStore
    {
        public static PageCacheStore Current { get; set; } = new PageCacheStore();

        public IDictionary<string, PageCacheInfo> PageCache { get; } = new Dictionary<string, PageCacheInfo>();

        public IReadOnlyDictionary<string, PageCacheInfo> GetPageCache()
        {
            return new ReadOnlyDictionary<string, PageCacheInfo>(PageCache);
        }
    }
}
