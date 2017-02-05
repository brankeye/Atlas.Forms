using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class NavigationProvider : INavigationProvider
    {
        public INavigation Navigation { get; set; }

        public NavigationProvider(INavigation navigation)
        {
            Navigation = navigation;
        }

        public void TrySetNavigation(object pageArg)
        {
            var page = pageArg as Page;
            while (page != null)
            {
                if (page is NavigationPage)
                {
                    Navigation = (page as NavigationPage).Navigation;
                    break;
                }
                if (page is MasterDetailPage)
                {
                    page = (page as MasterDetailPage).Detail;
                    continue;
                }
                if (page is TabbedPage)
                {
                    page = (page as TabbedPage).CurrentPage;
                    continue;
                }
                if (page is CarouselPage)
                {
                    page = (page as CarouselPage).CurrentPage;
                    continue;
                }
                break;
            }

            if (page != null && Navigation == null)
            {
                Navigation = page.Navigation;
            }
        }
    }
}
