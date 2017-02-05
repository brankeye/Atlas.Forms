using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;
using Xamarin.Forms;

#if TEST
using Application = Atlas.Forms.FormsApplication;
#endif

namespace Atlas.Forms
{
    public abstract class AtlasApplicationBase : Application
    {
        protected INavigationService NavigationService { get; set; }

        protected AtlasApplicationBase()
        {
            InitializeInternal();
        }

        private void InitializeInternal()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            NavigationService = CreateNavigationService(null);
            PageCacheService.Current = CreatePageCacheService();
            PageDialogService.Current = CreateDialogService();
            RegisterPagesForNavigation(CreatePageNavigationRegistry());
            RegisterPagesForCaching(CreatePageCacheRegistry());
        }

        protected abstract INavigationService CreateNavigationService(INavigation navigation);

        protected abstract IPageCacheService CreatePageCacheService();

        protected abstract IPageDialogService CreateDialogService();

        protected abstract IPageNavigationRegistry CreatePageNavigationRegistry();

        protected abstract IPageCacheRegistry CreatePageCacheRegistry();

        protected abstract void RegisterPagesForNavigation(IPageNavigationRegistry registry);

        protected abstract void RegisterPagesForCaching(IPageCacheRegistry registry);
    }
}
