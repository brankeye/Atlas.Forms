using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages.Infos;

namespace Atlas.Forms.Pages
{
    public class TriggerPageApi : ITriggerPageApi
    {
        private readonly MapInfo _info;

        public TriggerPageApi(string triggerPage, MapInfo mapInfo)
        {
            _info = mapInfo;
            _info.TriggerPageInfo = new TriggerPageInfo(triggerPage, TriggerOption.None);
        }

        public ITargetPageApi Appears()
        {
            _info.TriggerPageInfo.TriggerOption = TriggerOption.Appears;
            return new TargetPageApi(_info.TriggerPageInfo.Key, _info);
        }

        public ITargetPageApi IsCreated()
        {
            _info.TriggerPageInfo.TriggerOption = TriggerOption.IsCreated;
            return new TargetPageApi(_info.TriggerPageInfo.Key, _info);
        }
    }
}
