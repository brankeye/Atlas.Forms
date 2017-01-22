using atlas.core.Library.Pages;
using atlas.core.Library.Pages.Containers;
using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheRegistry
    {
        FluentTriggerPageApi WhenPage(string pageKey);

        FluentTriggerPageApi WhenPage<TPage>() where TPage : Page;
    }
}
