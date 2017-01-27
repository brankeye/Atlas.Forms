using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class About : IPageAppearingAware
    {
        public About()
        {
            InitializeComponent();
            IdLabel.Text = Guid.NewGuid().ToString();
        }

        public void OnPageAppearing(IParametersService parameters = null)
        {
            //PageDialogService.Current.DisplayAlert("Title", "Hey", "Ok");
        }
    }
}
