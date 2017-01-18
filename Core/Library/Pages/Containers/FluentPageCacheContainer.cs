using atlas.core.Library.Enums;
using atlas.core.Library.Navigation;

namespace atlas.core.Library.Pages.Containers
{
    public class FluentPageCacheContainer
    {
        public FluentPageCacheContainer(PageMapContainer container)
        {
            _container = container;
        }

        public FluentPageCacheContainer CachePage(string key)
        {
            _container.Key = key;
            _container.Type = PageNavigationStore.GetPageType(key);
            return this;
        }

        public FluentPageCacheContainer CachePage<TPage>()
        {
            var pageType = typeof(TPage);
            _container.Key = pageType.Name;
            _container.Type = PageNavigationStore.GetPageType(_container.Key);
            return this;
        }

        public FluentPageCacheContainer AsKeepAlive()
        {
            _container.CacheState = CacheState.KeepAlive;
            return this;
        }

        private readonly PageMapContainer _container;
    }
}
