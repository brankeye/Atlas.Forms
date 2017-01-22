using atlas.core.Library.Caching;
using atlas.core.Library.Enums;

namespace atlas.core.Library.Pages.Containers
{
    public class FluentTriggerPageApi
    {
        private readonly PageMapContainer _container;

        private string TriggerPage { get; }

        public FluentTriggerPageApi(string triggerPage, PageMapContainer mapContainer)
        {
            _container = mapContainer;
            _container.CacheState = CacheState.KeepAlive;
            var store = PageCacheMap.GetCacheStore();
            TriggerPage = triggerPage;
        }

        public FluentTargetPageApi Appears()
        {
            _container.CacheOption = CacheOption.Appears;
            return new FluentTargetPageApi(TriggerPage, _container);
        }

        public FluentTargetPageApi Disappears()
        {
            _container.CacheOption = CacheOption.Disappears;
            return new FluentTargetPageApi(TriggerPage, _container);
        }

        public FluentTargetPageApi IsCreated()
        {
            _container.CacheOption = CacheOption.IsCreated;
            return new FluentTargetPageApi(TriggerPage, _container);
        }
    }
}
