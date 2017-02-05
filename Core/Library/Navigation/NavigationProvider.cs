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
            if (page != null)
            {
                Navigation = page.Navigation;
            }
        }
    }
}
