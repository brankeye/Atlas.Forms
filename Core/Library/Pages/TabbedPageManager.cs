using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class TabbedPageManager : MultiPageManager<Page>, ITabbedPageManager
    {
        public TabbedPageManager(
            TabbedPage page,
            INavigationController navigationController,
            IPageCacheController pageCacheController) 
            : base(page, navigationController, pageCacheController)
        {

        }
    }
}
