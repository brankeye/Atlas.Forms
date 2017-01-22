using System;
using atlas.core.Library.Enums;
using Xamarin.Forms;

namespace atlas.core.Library.Pages.Containers
{
    public class FluentTargetPageApi
    {
        private readonly PageMapContainer _container;

        private string TriggerPage { get; }

        public FluentTargetPageApi(string triggerPage, PageMapContainer mapContainer)
        {
            _container = mapContainer;
            TriggerPage = triggerPage;
        }

        public FluentTargetPageApi CachePage()
        {
            return CachePage(TriggerPage);
        }

        public FluentTargetPageApi CachePage(string key)
        {
            _container.Key = key;
            return this;
        }

        public FluentTargetPageApi CachePage<TPage>()
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
