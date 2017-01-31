using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Pages
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

        public ITargetPageApi IsCreated()
        {
            _container.CacheOption = CacheOption.IsCreated;
            return new TargetPageApi(_triggerPage, _container);
        }
    }
}
