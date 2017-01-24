using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class TargetPageApi : ITargetPageApi
    {
        private readonly PageMapContainer _container;
        private readonly string _triggerPage;

        public TargetPageApi(string triggerPage, PageMapContainer mapContainer)
        {
            _container = mapContainer;
            _triggerPage = triggerPage;
        }

        public ITargetPageApi CachePage()
        {
            return CachePage(_triggerPage);
        }

        public ITargetPageApi CachePage(string key)
        {
            _container.Key = key;
            return this;
        }

        public ITargetPageApi CachePage<TPage>()
            where TPage : Page
        {
            return CachePage(typeof(TPage).Name);
        }

        public void AsKeepAlive()
        {
            _container.CacheState = CacheState.KeepAlive;
        }

        public void AsSingleInstance()
        {
            _container.CacheState = CacheState.SingleInstance;
        }
    }
}
