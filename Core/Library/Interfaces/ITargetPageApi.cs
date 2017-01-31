using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface ITargetPageApi
    {
        ITargetPageApi CachePage();

        ITargetPageApi CachePage(string key);

        ITargetPageApi CachePage<TPage>() where TPage : Page;

        void AsKeepAlive();

        void AsLifetimeInstance();

        void AsLifetimeInstance<TPage>() where TPage : Page;

        void AsLifetimeInstance(string page);

        void AsSingleInstance();
    }
}
