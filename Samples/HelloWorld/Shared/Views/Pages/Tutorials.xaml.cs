using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.core.Library.Services;
using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class Tutorials
    {
        public Tutorials()
        {
            InitializeComponent();
            Children.Add(PageCacheService.Current.GetCachedOrNewPage("TutorialOne"));
            Children.Add(PageCacheService.Current.GetCachedOrNewPage("TutorialTwo"));
            Children.Add(PageCacheService.Current.GetCachedOrNewPage("TutorialThree"));
        }
    }
}
