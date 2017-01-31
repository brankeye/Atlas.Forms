using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Managers;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class MainMasterDetailPage : IMasterDetailPageProvider
    {
        public IMasterDetailPageManager PageManager { get; set; }

        public void PresentPage(string page)
        {
            PageManager.PresentPage(page);
        }
    }
}
