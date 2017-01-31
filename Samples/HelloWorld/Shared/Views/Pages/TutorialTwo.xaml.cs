using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Interfaces.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class TutorialTwo : IPageAppearingAware
    {
        public TutorialTwo()
        {
            InitializeComponent();
            IdLabel.Text = Guid.NewGuid().ToString();
        }

        public void OnPageAppearing(IParametersService parameters = null)
        {
            
        }
    }
}
