using System;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class NextTabPage : ContentPage
    {
        public NextTabPage()
        {
            InitializeComponent();
            Label.Text = Guid.NewGuid().ToString();
        }
    }
}
