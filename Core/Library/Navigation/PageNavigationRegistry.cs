using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Navigation
{
    public class PageNavigationRegistry : IPageNavigationRegistry
    {
        public void RegisterPage<TPage>()
        {
            RegisterPage<TPage>(typeof(TPage).Name);
        }

        public void RegisterPage<TPage>(string key)
        {
            PageNavigationStore.TryAddType(key, typeof(TPage));
        }
    }
}
