using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Pages.Infos
{
    public class MapInfo : PageInfo
    {
        public MapInfo() { }

        public MapInfo(CacheState state, CacheOption option, IPageInfo info) : base (info)
        {
            CacheState = state;
            CacheOption = option;
        }

        public MapInfo(MapInfo info) : base(info)
        {
            CacheState = info.CacheState;
            CacheOption = info.CacheOption;
        }

        public CacheState CacheState { get; set; }

        public CacheOption CacheOption { get; set; }

        public string LifetimePageKey { get; set; }
    }
}
