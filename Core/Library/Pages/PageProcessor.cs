using Atlas.Forms.Behaviors;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
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
