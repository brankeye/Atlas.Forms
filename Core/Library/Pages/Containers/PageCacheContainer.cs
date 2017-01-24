using Xamarin.Forms;

namespace Atlas.Forms.Pages.Containers
{
    public class PageCacheContainer : PageMapContainer
    {
        public PageCacheContainer() { }

        public PageCacheContainer(Page page, PageMapContainer container) : base(container)
        {
            Page = page;
        }

        public Page Page { get; set; }

        internal bool Initialized { get; set; }
    }
}
