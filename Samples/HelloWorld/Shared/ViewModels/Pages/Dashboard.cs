using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class Dashboard : IMasterDetailPageProvider
    {
        public IMasterDetailPageManager PageManager { get; set; }

        public void PresentPage(string page)
        {
            PageManager.PresentPage(page);
        }
    }
}
