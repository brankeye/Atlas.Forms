using System;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Changelog : ContentPage
    {
        public Changelog()
        {
            InitializeComponent();
            IdLabel.Text = Guid.NewGuid().ToString();
        }
    }
}
