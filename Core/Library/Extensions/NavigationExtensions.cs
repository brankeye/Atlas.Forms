using atlas.core.Library.Caching;
using atlas.core.Library.Pages;
using atlas.core.Library.Services;
using Xamarin.Forms;

namespace atlas.core.Library.Extensions
{
    public static class NavigationExtensions
    {
        public static void Present(this MasterDetailPage page, string pageKey, ParametersService parameters = null)
        {
            var cacheCoordinator = new PageCacheCoordinator();
            var nextPage = cacheCoordinator.GetCachedOrNewPage(pageKey);
            if (nextPage is NavigationPage)
            {
                var tabbedPage = (nextPage as NavigationPage).CurrentPage as TabbedPage;
                if (tabbedPage != null)
                    tabbedPage.CurrentPageChanged += (sender, args) =>
                    {
                        var tabbedPageSender = sender as TabbedPage;
                        PageActionInvoker.InvokeOnPageAppearing(tabbedPageSender?.CurrentPage, parameters);
                        PageActionInvoker.InvokeOnPageAppeared(tabbedPageSender?.CurrentPage, parameters);
                    };
            }
            else if (nextPage is TabbedPage)
            {
                var tabbedPage = nextPage as TabbedPage;
                tabbedPage.CurrentPageChanged += (sender, args) =>
                {
                    var tabbedPageSender = sender as TabbedPage;
                    PageActionInvoker.InvokeOnPageAppearing(tabbedPageSender?.CurrentPage, parameters);
                    PageActionInvoker.InvokeOnPageAppeared(tabbedPageSender?.CurrentPage, parameters);
                };
            }
            cacheCoordinator.LoadCachedPages(pageKey);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, parameters);
            page.Detail = nextPage;
            PageActionInvoker.InvokeOnPageAppeared(nextPage, parameters);
        }

        public static void Present(this TabbedPage page, string pageKey, ParametersService parameters = null)
        {
            /*
            cacheCoordinator.LoadCachedPages(pageKey);
            PageActionInvoker.InvokeOnPageAppearing(nextPage, parameters);
            page.CurrentPage = page.Children.FirstOrDefault(x => x == nextPage);
            PageActionInvoker.InvokeOnPageAppeared(nextPage, parameters);
            */
        }
    }
}
