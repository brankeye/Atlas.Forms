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
            PageDialogService.Current = CreateDialogService();
            RegisterPagesForNavigation(CreatePageNavigationRegistry());
            RegisterPagesForCaching(CreatePageCacheRegistry());
        }

        protected abstract INavigationService CreateNavigationService(INavigation navigation = null);

        protected abstract IPageCacheService CreatePageService();

        protected abstract IPageDialogService CreateDialogService();

        protected abstract IPageNavigationRegistry CreatePageNavigationRegistry();

        protected abstract IPageCacheRegistry CreatePageCacheRegistry();

        protected abstract void RegisterPagesForNavigation(IPageNavigationRegistry registry);

        protected abstract void RegisterPagesForCaching(IPageCacheRegistry registry);
    }
}
