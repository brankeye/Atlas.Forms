using atlas.core.Library.Interfaces;
using atlas.core.Library.Services;
using Xamarin.Forms;

namespace atlas.core.Library
{
    public abstract class AtlasApplicationBase : Application
    {
        protected AtlasApplicationBase()
        {
            InitializeInternal();
        }

        private void InitializeInternal()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            NavigationService.Current = CreateNavigationService();
            PageCacheService.Current = CreatePageCacheService();
            RegisterPagesForNavigation(CreatePageNavigationRegistry());
            RegisterPagesForCaching(CreatePageCacheRegistry());
        }

        protected abstract void RegisterPagesForNavigation(IPageNavigationRegistry registry);

        protected abstract void RegisterPagesForCaching(IPageCacheRegistry registry);

        protected abstract INavigationService CreateNavigationService();

        protected abstract IPageCacheService CreatePageCacheService();

        protected abstract IPageNavigationRegistry CreatePageNavigationRegistry();

        protected abstract IPageCacheRegistry CreatePageCacheRegistry();
    }
}
