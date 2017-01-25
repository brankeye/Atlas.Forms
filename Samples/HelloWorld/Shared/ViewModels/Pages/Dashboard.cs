using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.ViewModels.Pages
{
    public class Dashboard
    {
        public void ChangePage(string page)
        {
            NavigationService.Current.PresentPage(page);
        }
    }
}
