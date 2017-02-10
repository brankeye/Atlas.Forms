using System;
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
        protected WeakReference<MasterDetailPage> PageReference { get; set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageRetriever PageRetriever { get; set; }

        public MasterDetailPageManager(
            MasterDetailPage page,
            INavigationController navigationController,
            IPageRetriever pageRetriever)
        {
            PageReference = new WeakReference<MasterDetailPage>(page);
            NavigationController = navigationController;
            PageRetriever = pageRetriever;
        }

        public virtual void PresentPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageRetriever.GetCachedOrNewPage(pageInfo, paramService);
            MasterDetailPage pageRef;
            if (PageReference.TryGetTarget(out pageRef))
            {
                var lastPage = pageRef.Detail;
                if (lastPage != null)
                {
                    PageActionInvoker.InvokeOnPageDisappearing(lastPage, paramService);
                }
                PageActionInvoker.InvokeOnPageAppearing(nextPage, paramService);
                pageRef.Detail = nextPage;
                if (lastPage != null)
                {
                    PageActionInvoker.InvokeOnPageDisappeared(lastPage, paramService);
                }
                PageActionInvoker.InvokeOnPageAppeared(nextPage, paramService);
            }
        }
    }
}
