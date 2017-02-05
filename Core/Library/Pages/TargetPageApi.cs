using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Info;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class TargetPageApi : ITargetPageApi
    {
        private readonly PageMapInfo _info;
        private readonly string _triggerPage;

        public TargetPageApi(string triggerPage, PageMapInfo mapInfo)
        {
            _info = mapInfo;
            _triggerPage = triggerPage;
        }

        public ITargetPageApi CachePage()
        {
            return CachePage(_triggerPage);
        }

        public ITargetPageApi CachePage(string key)
        {
            _info.Key = key;
            return this;
        }

        public ITargetPageApi CachePage<TPage>()
            where TPage : Page
        {
            return CachePage(typeof(TPage).Name);
        }

        public void AsKeepAlive()
        {
            _info.CacheState = CacheState.KeepAlive;
        }

        public void AsSingleInstance()
        {
            _info.CacheState = CacheState.SingleInstance;
        }

        public void AsLifetimeInstance()
        {
            _info.CacheState = CacheState.LifetimeInstance;
            _info.LifetimePageKey = _triggerPage;
        }

        public void AsLifetimeInstance<TPage>() where TPage : Page
        {
            _info.CacheState = CacheState.LifetimeInstance;
            _info.LifetimePageKey = typeof(TPage).Name;
        }

        public void AsLifetimeInstance(string page)
        {
            _info.CacheState = CacheState.LifetimeInstance;
            _info.LifetimePageKey = page;
        }
    }
}
