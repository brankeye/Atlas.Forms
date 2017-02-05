using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationRegistry : IPageNavigationRegistry
    {
        public virtual void RegisterPage<TPage>() where TPage : Page
        {
            RegisterPage<TPage>(typeof(TPage).Name);
        }

        public virtual void RegisterPage<TPage, TClass>() where TPage : Page
        {
            RegisterPage<TPage>(typeof(TClass).Name);
        }

        public virtual void RegisterPage<TPage>(string key) where TPage : Page
        {
            PageNavigationStore.Current.AddTypeAndConstructorInfo(key, typeof(TPage));
        }
    }
}
