using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Pages.Containers
{
    public class PageMapContainer : PageContainer
    {
        public PageMapContainer() { }

        public PageMapContainer(CacheState state, CacheOption option, IPageContainer container) : base (container)
        {
            CacheState = state;
            CacheOption = option;
        }

        public PageMapContainer(PageMapContainer container) : base(container)
        {
            CacheState = container.CacheState;
            CacheOption = container.CacheOption;
        }

        public CacheState CacheState { get; set; }

        public CacheOption CacheOption { get; set; }
    }
}
