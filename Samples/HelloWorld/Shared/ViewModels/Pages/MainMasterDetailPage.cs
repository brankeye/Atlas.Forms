using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class MainMasterDetailPage : IMasterDetailPageProvider, IInitializeAware
    {
        public IMasterDetailPageManager PageManager { get; set; }

        public void Initialize(IParametersService parameters)
        {
            PageManager.PresentPage(Nav.Get("MyContentPage").AsNavigationPage().Info());
        }

        public void PresentPage(NavigationInfo pageInfo)
        {
            PageManager.PresentPage(pageInfo);
        }
    }
}
