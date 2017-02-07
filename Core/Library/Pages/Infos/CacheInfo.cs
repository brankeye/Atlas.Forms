using Xamarin.Forms;

namespace Atlas.Forms.Pages.Infos
{
    public class CacheInfo : MapInfo
    {
        public CacheInfo() { }

        public CacheInfo(Page page, MapInfo info) : base(info)
        {
            Page = page;
        }

        public Page Page { get; set; }

        public bool Initialized { get; set; }
    }
}
