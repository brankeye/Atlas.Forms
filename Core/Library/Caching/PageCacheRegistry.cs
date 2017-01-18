using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages;

namespace atlas.core.Library.Caching
{
    public class PageCacheRegistry : IPageCacheRegistry
    {
        public FluentPageCacheContainer WhenAppears(string pageKey)
        {
            var container = new PageMapContainer();
            PageCacheMap.AddPageContainer(pageKey, container);
            return new FluentPageCacheContainer(container);
        }

        public FluentPageCacheContainer WhenAppears<TPage>()
        {
            return WhenAppears(typeof(TPage).Name);
        }
    }
}
