using System;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Interfaces.Pages;

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
