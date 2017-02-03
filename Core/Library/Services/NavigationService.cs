using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class NavigationService : INavigationService
    {
        public static INavigationService Current { get; internal set; }

        public virtual IReadOnlyList<IPageContainer> NavigationStack => NavigationController.GetNavigationStack();

        public virtual IReadOnlyList<IPageContainer> ModalStack => NavigationController.GetModalStack();

        public virtual INavigation Navigation => NavigationController.GetNavigation();

        protected INavigationController NavigationController { get; set; }

        protected IPageCacheController PageCacheController { get; }

        public NavigationService(INavigationController navigationController, IPageCacheController pageCacheController)
        {
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public virtual void InsertPageBefore(NavigationInfo pageInfo, NavigationInfo before, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageCacheController.GetCachedOrNewPage(pageInfo, paramService);
            NavigationController.InsertPageBefore(nextPage, before.Page, paramService);
        }

        public virtual async Task<IPageContainer> PopAsync(IParametersService parameters = null)
        {
            return await PopAsync(true, parameters);
        }

        public virtual async Task<IPageContainer> PopAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(animated, parameters);
        }

        public virtual async Task<IPageContainer> PopModalAsync(IParametersService parameters = null)
        {
            return await PopModalAsync(true, parameters);
        }

        public virtual async Task<IPageContainer> PopModalAsync(bool animated, IParametersService parameters = null)
        {
            return await PopInternalAsync(animated, parameters, true);
        }

        protected virtual async Task<IPageContainer> PopInternalAsync(bool animated, IParametersService parameters = null, bool useModal = false)
        {
            var paramService = parameters ?? new ParametersService();
            var pageStack = useModal ? ModalStack :
                                       NavigationStack;
            var lastPage = pageStack.Last();
            PageCacheController.RemoveCachedPages(lastPage.Key);
            var pageContainer = await NavigationController.PopPageAsync(animated, paramService, useModal);
            PageCacheController.AddCachedPagesWithOption(lastPage.Key, CacheOption.Appears);
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
            var pageStack = useModal ? ModalStack :
                                       NavigationStack;
            if (pageStack.Count > 0)
            {
                var lastPage = pageStack.Last();
                PageCacheController.RemoveCachedPages(lastPage.Key);
            }
            var nextPage = PageCacheController.GetCachedOrNewPage(pageInfo, paramService);
            await NavigationController.PushPageAsync(nextPage, animated, paramService, useModal);
            PageCacheController.AddCachedPagesWithOption(pageInfo.Page, CacheOption.Appears);
        }

        public virtual void RemovePage(NavigationInfo pageInfo)
        {
            NavigationController.RemovePage(pageInfo.Page);
        }

        public virtual void SetMainPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            //if (NavigationController.GetMainPage() != null && MainPageContainer != null)
            //{
            //    PageCacheController.RemoveCachedPages(MainPageContainer.Key);
            //}
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageCacheController.GetCachedOrNewPage(pageInfo, paramService);
            NavigationController.SetMainPage(nextPage, paramService);
            PageCacheController.AddCachedPagesWithOption(pageInfo.Page, CacheOption.Appears);
        }
    }
}
