using System;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class SecondTabPage : INavigationServiceProvider
    {
        public SecondTabPage()
        {
            InitializeComponent();
            Label.Text = Guid.NewGuid().ToString();
        }

        public INavigationService NavigationService { get; set; }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var history = NavigationService.NavigationStack;
            NavigationService.PushAsync(Nav.Get("NextTabPage").AsNewInstance().Info());
        }
    }
}
