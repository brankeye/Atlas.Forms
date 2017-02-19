using Atlas.Forms.Enums;
using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class TargetPageApi : ITargetPageApi
    {
        private readonly MapInfo _info;
        private readonly string _triggerPage;

        public TargetPageApi(string triggerPage, MapInfo mapInfo)
        {
            _info = mapInfo;
            _info.TargetPageInfo = new TargetPageInfo("", CacheState.Default);
            _triggerPage = triggerPage;
        }

        public ITargetPageApi CachePage()
        {
            return CachePage(_triggerPage);
        }

        public ITargetPageApi CachePage(string key)
        {
            _info.TargetPageInfo.Key = key;
            return this;
        }

        public ITargetPageApi CachePage<TPage>()
            where TPage : Page
        {
            return CachePage(typeof(TPage).Name);
        }

        public void AsKeepAlive()
        {
            _info.TargetPageInfo.CacheState = CacheState.KeepAlive;
        }

        public void AsSingleInstance()
        {
            _info.TargetPageInfo.CacheState = CacheState.SingleInstance;
        }

        public void AsLifetimeInstance()
        {
            _info.TargetPageInfo.CacheState = CacheState.LifetimeInstance;
            _info.TargetPageInfo.LifetimeInstanceKey = _triggerPage;
        }

        public void AsLifetimeInstance<TPage>() where TPage : Page
        {
            _info.TargetPageInfo.CacheState = CacheState.LifetimeInstance;
            _info.TargetPageInfo.LifetimeInstanceKey = typeof(TPage).Name;
        }

        public void AsLifetimeInstance(string page)
        {
            _info.TargetPageInfo.CacheState = CacheState.LifetimeInstance;
            _info.TargetPageInfo.LifetimeInstanceKey = page;
        }
    }
}
