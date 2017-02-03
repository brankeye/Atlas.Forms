using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class FirstTabPage
    {
        public FirstTabPage()
        {
            InitializeComponent();
            Label.Text = Guid.NewGuid().ToString();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            NavigationService.Current.PushAsync(Nav.Get("NextTabPage").Info());
        }
    }
}
