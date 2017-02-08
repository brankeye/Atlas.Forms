using Xamarin.Forms;

namespace Atlas.Forms.Pages.Infos
{
    public class CacheInfo : MapInfo
    {
        public CacheInfo() { }

        public CacheInfo(Page page, bool initialized, MapInfo info) : base(info)
        {
            Page = page;
            Initialized = initialized;
        }

        public Page Page { get; set; }

        public bool Initialized { get; set; }
    }
}
