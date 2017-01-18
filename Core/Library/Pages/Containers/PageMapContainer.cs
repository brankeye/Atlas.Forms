using System;
using atlas.core.Library.Enums;

namespace atlas.core.Library.Pages.Containers
{
    public class PageMapContainer : PageContainer
    {
        public PageMapContainer()
        {
            CacheState = CacheState.Default;
        }

        public PageMapContainer(string key, Type type) : base(key, type)
        {
            CacheState = CacheState.Default;
        }

        public PageMapContainer(string key, Type type, CacheState cacheState) : base(key, type)
        {
            CacheState = cacheState;
        }

        public CacheState CacheState { get; set; }
    }
}
