using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Pages;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class TutorialOne : IPageAppearingAware
    {
        public TutorialOne()
        {
            InitializeComponent();
            IdLabel.Text = Guid.NewGuid().ToString();
        }

        public void OnPageAppearing(IParametersService parameters)
        {
            var id = parameters.TryGet<int>("Id");
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var stack = NavigationService.Current.NavigationStack;
            NavigationService.Current.PushAsync("TestPage");
        }
    }
}
