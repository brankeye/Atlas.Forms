using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class TabbedPageManager : MultiPageManager<Page>, ITabbedPageManager
    {
        public TabbedPageManager(
            TabbedPage page,
            IPageRetriever pageRetriever,
            IPageKeyStore pageKeyStore) 
            : base(page, pageRetriever, pageKeyStore)
        {

        }
    }
}
