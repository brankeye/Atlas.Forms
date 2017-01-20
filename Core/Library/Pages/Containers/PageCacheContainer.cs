using System;
using atlas.core.Library.Enums;
using Xamarin.Forms;

namespace atlas.core.Library.Pages.Containers
{
    public class PageCacheContainer : PageMapContainer
    {
        public PageCacheContainer()
        {
        }

        public PageCacheContainer(string key, Type type, Page page) : base(key, type)
        {
            Page = page;
        }

        public PageCacheContainer(string key, Type type, CacheState cacheState, Page page) : base(key, type, cacheState)
        {
            Page = page;
        }

        public Page Page { get; set; }

        internal bool Initialized { get; set; }
    }
}
