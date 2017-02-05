using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Pages.Info
{
    public class PageMapInfo : PageInfo
    {
        public PageMapInfo() { }

        public PageMapInfo(CacheState state, CacheOption option, IPageInfo info) : base (info)
        {
            CacheState = state;
            CacheOption = option;
        }

        public PageMapInfo(PageMapInfo info) : base(info)
        {
            CacheState = info.CacheState;
            CacheOption = info.CacheOption;
        }

        public CacheState CacheState { get; set; }

        public CacheOption CacheOption { get; set; }

        public string LifetimePageKey { get; set; }
    }
}
