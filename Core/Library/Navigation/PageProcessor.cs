using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atlas.core.Library.Behaviors;
using Xamarin.Forms;

namespace atlas.core.Library.Navigation
{
    public class PageProcessor
    {
        public static void Process(Page page)
        {
            (page as NavigationPage)?.Behaviors.Add(new NavigationPageBackButtonBehavior());
            (page as TabbedPage)?.Behaviors.Add(new TabbedPagePresentationBehavior());
        }
    }
}
