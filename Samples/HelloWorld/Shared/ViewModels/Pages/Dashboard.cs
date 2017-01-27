using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Managers;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class Dashboard : IMasterDetailPageProvider
    {
        public IMasterDetailPageManager Manager { get; set; }

        public void PresentPage(string page)
        {
            Manager.PresentPage(page);
        }
    }
}
