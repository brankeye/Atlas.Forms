using System;

using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class MyNextPage : ContentPage
    {
        public MyNextPage()
        {
            InitializeComponent();
            Label.Text = Guid.NewGuid().ToString();
        }
    }
}
