using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;

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

        }
    }
}
