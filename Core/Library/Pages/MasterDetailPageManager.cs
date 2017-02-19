using System;
using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class MasterDetailPageManager : IMasterDetailPageManager
    {
        protected MasterDetailPage PageInstance { get; set; }

        protected IPageRetriever PageRetriever { get; set; }

        protected IPublisher Publisher { get; set; }

        public MasterDetailPageManager(
            MasterDetailPage page,
            IPageRetriever pageRetriever,
            IPublisher publisher)
        {
            PageInstance = page;
            PageRetriever = pageRetriever;
            Publisher = publisher;
        }

        public virtual void PresentPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageRetriever.GetCachedOrNewPage(pageInfo, paramService);
            var lastPage = PageInstance.Detail;
            if (lastPage != null)
            {
                Publisher.SendPageDisappearingMessage(lastPage, paramService);
            }
            Publisher.SendPageAppearingMessage(nextPage, paramService);
            PageInstance.Detail = nextPage;
            if (lastPage != null)
            {
                Publisher.SendPageAppearingMessage(lastPage, paramService);
            }
            Publisher.SendPageAppearingMessage(nextPage, paramService);
        }
    }
}
