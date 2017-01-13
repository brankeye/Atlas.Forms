using System;
using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Caching
{
    public class PageCacheRegistry : IPageCacheRegistry
    {
        public void RegisterPageForCache(string pageKey, string cachedPageKey)
        {
            throw new NotImplementedException();
        }

        public void RegisterPageForCache<TPage>(string cachedPageKey)
        {
            throw new NotImplementedException();
        }

        public void RegisterPageForCache<TPage, TCachedPage>()
        {
            throw new NotImplementedException();
        }
    }
}
