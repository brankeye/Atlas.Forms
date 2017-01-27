using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class Dashboard : IMasterDetailPageManager
    {
        public IPresenter PageController { get; set; }

        public void PresentPage(string page)
        {
            PageController.PresentPage(page);
        }
    }
}
