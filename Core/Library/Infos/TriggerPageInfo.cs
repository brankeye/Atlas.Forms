using Atlas.Forms.Enums;

namespace Atlas.Forms.Infos
{
    public class TriggerPageInfo
    {
        public TriggerPageInfo(string key, TriggerOption triggerOption)
        {
            Key = key;
            TriggerOption = triggerOption;
        }

        public string Key { get; set; }

        public TriggerOption TriggerOption { get; set; }
    }
}
