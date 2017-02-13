using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Pages.Infos
{
    public class MapInfo
    {
        public MapInfo()
        {
            
        }

        public MapInfo(TriggerPageInfo triggerPageInfo, TargetPageInfo targetPageInfo)
        {
            TriggerPageInfo = triggerPageInfo;
            TargetPageInfo = targetPageInfo;
        }

        public TriggerPageInfo TriggerPageInfo { get; set; }

        public TargetPageInfo TargetPageInfo { get; set; }
    }
}
