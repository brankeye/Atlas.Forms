using System;
using atlas.core.Library.Interfaces;
using atlas.core.Library.Interfaces.Pages;
using atlas.core.Library.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class TutorialOne : IPageAppearingAware
    {
        public TutorialOne()
        {
            InitializeComponent();
            IdLabel.Text = Guid.NewGuid().ToString();
        }

        public void OnPageAppearing(IParametersService parameters = null)
        {
            
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var stack = NavigationService.Current.NavigationStack;
            NavigationService.Current.PushAsync("TestPage");
        }
    }
}
