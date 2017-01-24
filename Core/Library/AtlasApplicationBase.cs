using Atlas.Forms.Interfaces;
using Atlas.Forms.Services;
using Xamarin.Forms;

#if TEST
using Application = Atlas.Forms.FormsApplication;
#endif

namespace Atlas.Forms
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

        protected abstract INavigationService CreateNavigationService();

        protected abstract IPageCacheService CreatePageCacheService();

        protected abstract IPageNavigationRegistry CreatePageNavigationRegistry();

        protected abstract IPageCacheRegistry CreatePageCacheRegistry();

        protected abstract void RegisterPagesForNavigation(IPageNavigationRegistry registry);

        protected abstract void RegisterPagesForCaching(IPageCacheRegistry registry);
    }
}
