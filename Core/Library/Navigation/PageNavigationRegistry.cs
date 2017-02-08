using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationRegistry : IPageNavigationRegistry
    {
        protected IPageNavigationStore PageNavigationStore { get; }

        public PageNavigationRegistry(IPageNavigationStore pageNavigationStore)
        {
            PageNavigationStore = pageNavigationStore;
        }

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
            PageNavigationStore.AddTypeAndConstructorInfo(key, typeof(TPage));
        }
    }
}
