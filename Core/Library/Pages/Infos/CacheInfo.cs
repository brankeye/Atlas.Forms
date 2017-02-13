using Xamarin.Forms;

namespace Atlas.Forms.Pages.Infos
{
    public class CacheInfo
    {
        public CacheInfo(Page page, bool initialized, TargetPageInfo info)
        {
            Page = page;
            Initialized = initialized;
            TargetPageInfo = info;
        }

        public TargetPageInfo TargetPageInfo { get; set; }

        public Page Page { get; set; }

        public bool Initialized { get; set; }
    }
}
