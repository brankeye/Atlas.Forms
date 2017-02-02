using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface IPageNavigationRegistry
    {
        void RegisterPage<TPage>() where TPage : Page;

        void RegisterPage<TPage, TClass>() where TPage : Page;

        void RegisterPage<TPage>(string key) where TPage : Page;
    }
}
