using Xamarin.Forms;

namespace Atlas.Forms.Pages.Info
{
    public class PageCacheInfo : PageMapInfo
    {
        public PageCacheInfo() { }

        public PageCacheInfo(Page page, PageMapInfo info) : base(info)
        {
            Page = page;
        }

        public Page Page { get; set; }

        public bool Initialized { get; set; }
    }
}
