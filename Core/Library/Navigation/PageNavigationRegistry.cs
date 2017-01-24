using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationRegistry : IPageNavigationRegistry
    {
        public void RegisterPage<TPage>()
        {
            RegisterPage<TPage>(typeof(TPage).Name);
        }

        public void RegisterPage<TPage>(string key)
        {
            PageNavigationStore.Current.PageTypes[key] = typeof(TPage);
        }
    }
}
