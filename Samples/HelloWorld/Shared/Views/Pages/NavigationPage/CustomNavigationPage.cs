using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages.NavigationPage
{
    public class CustomNavigationPage : Xamarin.Forms.NavigationPage
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            Icon = root.Icon;
        }

        public CustomNavigationPage()
        {
            Init();
        }

        void Init()
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                BarBackgroundColor = Color.FromHex("FAFAFA");
            }
            else
            {
                BarBackgroundColor = Color.Purple;
                BarTextColor = Color.White;
            }
        }
    }
}
