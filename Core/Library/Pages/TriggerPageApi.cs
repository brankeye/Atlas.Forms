using atlas.core.Library.Enums;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Pages.Containers;

namespace atlas.core.Library.Pages
{
    public class TriggerPageApi : ITriggerPageApi
    {
        private readonly PageMapContainer _container;
        private readonly string _triggerPage;

        public TriggerPageApi(string triggerPage, PageMapContainer mapContainer)
        {
            _container = mapContainer;
            _container.CacheState = CacheState.KeepAlive;
            _triggerPage = triggerPage;
        }

        public ITargetPageApi Appears()
        {
            _container.CacheOption = CacheOption.Appears;
            return new TargetPageApi(_triggerPage, _container);
        }

        public ITargetPageApi Disappears()
        {
            _container.CacheOption = CacheOption.Disappears;
            return new TargetPageApi(_triggerPage, _container);
        }

        public ITargetPageApi IsCreated()
        {
            _container.CacheOption = CacheOption.IsCreated;
            return new TargetPageApi(_triggerPage, _container);
        }
    }
}
