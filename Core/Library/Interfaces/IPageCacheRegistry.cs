using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface IPageCacheRegistry
    {
        ITriggerPageApi WhenPage(string pageKey);

        ITriggerPageApi WhenPage<TPage>() where TPage : Page;
    }
}
