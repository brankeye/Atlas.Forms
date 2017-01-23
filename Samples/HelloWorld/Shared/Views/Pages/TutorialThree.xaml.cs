using System;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Interfaces.Pages;

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
