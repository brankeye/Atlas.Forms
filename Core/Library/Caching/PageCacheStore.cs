using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace atlas.core.Library.Caching
{
    public class PageCacheStore
    {
        public static Dictionary<string, Page> CacheStore { get; } = new Dictionary<string, Page>();

        public static Page GetCachedPage(string key)
        {
            Page page;
            CacheStore.TryGetValue(key, out page);
            return page;
        }
    }
}
