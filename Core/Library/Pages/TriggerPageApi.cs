using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Infos;

namespace Atlas.Forms.Pages
{
    public class TriggerPageApi : ITriggerPageApi
    {
        private readonly MapInfo _info;
        private readonly string _triggerPage;

        public TriggerPageApi(string triggerPage, MapInfo mapInfo)
        {
            _info = mapInfo;
            _info.CacheState = CacheState.KeepAlive;
            _triggerPage = triggerPage;
        }

        public ITargetPageApi Appears()
        {
            _info.CacheOption = CacheOption.Appears;
            return new TargetPageApi(_triggerPage, _info);
        }

        public ITargetPageApi IsCreated()
        {
            _info.CacheOption = CacheOption.IsCreated;
            return new TargetPageApi(_triggerPage, _info);
        }
    }
}
