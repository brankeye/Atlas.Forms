using System;
using atlas.core.Library.Enums;
using Xamarin.Forms;

namespace atlas.core.Library.Pages.Containers
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
