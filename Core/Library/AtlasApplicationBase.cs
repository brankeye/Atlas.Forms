using atlas.core.Library.Interfaces;
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
            CreateNavigationService();
            RegisterPagesForNavigation(CreatePageNavigationRegistry());
            RegisterPagesForCaching(CreatePageCacheRegistry());
        }

        protected virtual void RegisterPagesForNavigation(IPageNavigationRegistry registry) { }

        protected virtual void RegisterPagesForCaching(IPageCacheRegistry registry) { }

        protected abstract void CreateNavigationService();

        protected abstract IPageNavigationRegistry CreatePageNavigationRegistry();

        protected abstract IPageCacheRegistry CreatePageCacheRegistry();
    }
}
