using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class MainMasterDetailPage : IMasterDetailPageProvider, IInitializeAware
    {
        public IMasterDetailPageManager PageManager { get; set; }

        public void Initialize(IParametersService parameters)
        {
            PageManager.PresentPage("NavigationPage/MyContentPage");
        }

        public void PresentPage(string page)
        {
            PageManager.PresentPage(page);
        }
    }
}
