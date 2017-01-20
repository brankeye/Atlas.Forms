using System;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Contact : ContentPage
    {
        public Contact()
        {
            InitializeComponent();
            IdLabel.Text = Guid.NewGuid().ToString();
        }
    }
}
