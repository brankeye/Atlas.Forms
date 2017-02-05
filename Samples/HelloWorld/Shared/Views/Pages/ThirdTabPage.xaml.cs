using System;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class ThirdTabPage : ContentPage
    {
        public ThirdTabPage()
        {
            InitializeComponent();
            Label.Text = Guid.NewGuid().ToString();
        }
    }
}
