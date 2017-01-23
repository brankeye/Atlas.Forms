using Xamarin.Forms;

namespace atlas.core.Library.Interfaces
{
    public interface ITargetPageApi
    {
        ITargetPageApi CachePage();

        ITargetPageApi CachePage(string key);

        ITargetPageApi CachePage<TPage>() where TPage : Page;

        void AsKeepAlive();

        void AsSingleInstance();
    }
}
