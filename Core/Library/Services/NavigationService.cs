using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class NavigationService : INavigationService
    {
        public virtual IReadOnlyList<IPageInfo> NavigationStack => NavigationController.GetNavigationStack();

        public virtual IReadOnlyList<IPageInfo> ModalStack => NavigationController.GetModalStack();

        public virtual INavigation Navigation => NavigationController.GetNavigation();

        protected INavigationController NavigationController { get; set; }

        protected IPageRetriever PageRetriever { get; }

        protected IPublisher Publisher { get; }

        public NavigationService(INavigationController navigationController, IPageRetriever pageRetriever, IPublisher publisher)
        {
            NavigationController = navigationController;
            PageRetriever = pageRetriever;
            Publisher = publisher;
        }

        public virtual void InsertPageBefore(NavigationInfo pageInfo, NavigationInfo before, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageRetriever.GetCachedOrNewPage(pageInfo, paramService);
            NavigationController.InsertPageBefore(nextPage, before.Page, paramService);
        }

        public virtual async Task<IPageInfo> PopAsync(IParametersService parameters = null)
        {
            return await PopAsync(true, parameters);
        }

        public virtual async Task<IPageInfo> PopAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(animated, parameters);
        }

        public virtual async Task<IPageInfo> PopModalAsync(IParametersService parameters = null)
        {
            return await PopModalAsync(true, parameters);
        }

        public virtual async Task<IPageInfo> PopModalAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(animated, parameters, true);
        }

        protected virtual async Task<IPageInfo> PopInternalAsync(bool animated, IParametersService parameters = null, bool useModal = false)
        {
            var paramService = parameters ?? new ParametersService();
            var pageStack = useModal ? Navigation.ModalStack :
                                       Navigation.NavigationStack;
            var lastPage = pageStack.Last();
            Page nextPage = null;
            if (pageStack.Count > 1)
            {
                nextPage = pageStack[pageStack.Count - 2];
            }
            Publisher.SendPageAppearingMessage(nextPage, paramService);
            Publisher.SendPageDisappearingMessage(lastPage, paramService);
            var pageContainer = await NavigationController.PopPageAsync(animated, paramService, useModal);
            Publisher.SendPageAppearedMessage(nextPage, paramService);
            Publisher.SendPageDisappearedMessage(lastPage, paramService);
            Publisher.SendPageNavigatedFromMessage(lastPage);
            Publisher.SendPageNavigatedToMessage(nextPage);
            return pageContainer;
        }

        public virtual async Task PopToRootAsync()
        {
            await PopToRootAsync(true);
        }

        public virtual async Task PopToRootAsync(bool animated)
        {
            while (Navigation.NavigationStack.Count > 1)
            {
                await PopAsync(animated);
            }
        }

        public virtual async Task PushAsync(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            await PushAsync(pageInfo, true, parameters);
        }

        public virtual async Task PushAsync(NavigationInfo pageInfo, bool animated, IParametersService parameters = null)
        {
            await PushInternalAsync(pageInfo, animated, parameters);
        }

        public virtual async Task PushModalAsync(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            await PushModalAsync(pageInfo, true, parameters);
        }

        public virtual async Task PushModalAsync(NavigationInfo pageInfo, bool animated, IParametersService parameters = null)
        {
            await PushInternalAsync(pageInfo, animated, parameters, true);
        }

        protected virtual async Task PushInternalAsync(NavigationInfo pageInfo, bool animated, IParametersService parameters = null, bool useModal = false)
        {
            var paramService = parameters ?? new ParametersService();
            var pageStack = useModal ? Navigation.ModalStack :
                                       Navigation.NavigationStack;
            var lastPage = pageStack.LastOrDefault();
            var nextPage = PageRetriever.GetCachedOrNewPage(pageInfo, paramService);
            Publisher.SendPageAppearingMessage(nextPage, paramService);
            Publisher.SendPageDisappearingMessage(lastPage, paramService);
            await NavigationController.PushPageAsync(nextPage, animated, paramService, useModal);
            Publisher.SendPageAppearedMessage(nextPage, paramService);
            Publisher.SendPageDisappearedMessage(lastPage, paramService);
            Publisher.SendPageNavigatedFromMessage(lastPage);
            Publisher.SendPageNavigatedToMessage(nextPage);
        }

        public virtual void RemovePage(NavigationInfo pageInfo)
        {
            NavigationController.RemovePage(pageInfo.Page);
        }

        public virtual void SetMainPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var lastPage = NavigationController.GetMainPage();
            Publisher.SendPageNavigatedFromMessage(lastPage);
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageRetriever.GetCachedOrNewPage(pageInfo, paramService);
            Publisher.SendPageAppearingMessage(nextPage, paramService);
            Publisher.SendPageDisappearingMessage(lastPage, paramService);
            NavigationController.SetMainPage(nextPage, paramService);
            Publisher.SendPageAppearedMessage(nextPage, paramService);
            Publisher.SendPageDisappearedMessage(lastPage, paramService);
            Publisher.SendPageNavigatedToMessage(nextPage);
        }
    }
}
