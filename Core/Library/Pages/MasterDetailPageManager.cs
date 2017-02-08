using Atlas.Forms.Components;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class MasterDetailPageManager : IMasterDetailPageManager
    {
        protected MasterDetailPage Page { get; set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageRetriever PageRetriever { get; set; }

        public MasterDetailPageManager(
            MasterDetailPage page,
            INavigationController navigationController,
            IPageRetriever pageRetriever)
        {
            Page = page;
            NavigationController = navigationController;
            PageRetriever = pageRetriever;
        }

        public virtual void PresentPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageRetriever.GetCachedOrNewPage(pageInfo, paramService);
            var lastPage = Page.Detail;
            if (lastPage != null)
            {
                PageActionInvoker.InvokeOnPageDisappearing(lastPage, paramService);
            }
            PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
            Page.Detail = nextPage;
            if (lastPage != null)
            {
                PageActionInvoker.InvokeOnPageDisappeared(lastPage, paramService);
            }
            PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
        }
    }
}
