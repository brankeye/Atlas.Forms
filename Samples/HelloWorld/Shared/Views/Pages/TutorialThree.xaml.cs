using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class TutorialThree : IPageAppearingAware
    {
        public TutorialThree()
        {
            InitializeComponent();
            IdLabel.Text = Guid.NewGuid().ToString();
        }

        public void OnPageAppearing(IParametersService parameters = null)
        {

        }
    }
}
