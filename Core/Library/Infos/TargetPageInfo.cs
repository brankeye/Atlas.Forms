using Atlas.Forms.Enums;

namespace Atlas.Forms.Infos
{
    public class TargetPageInfo
    {
        public TargetPageInfo(string key, CacheState cacheState)
        {
            Key = key;
            CacheState = cacheState;
        }

        public string Key { get; set; }

        public CacheState CacheState { get; set; }

        public string LifetimeInstanceKey { get; set; }
    }
}
