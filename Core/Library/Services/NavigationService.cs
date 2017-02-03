﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;
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

        public virtual void InsertPageBefore(string page, string before, IParametersService parameters = null)
        {
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageCacheController.GetCachedOrNewPage(page, paramService);
            NavigationController.InsertPageBefore(page, nextPage, before, paramService);
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

        public virtual async Task PushAsync(string page, IParametersService parameters = null)
        {
            await PushAsync(page, true, parameters);
        }

        public virtual async Task PushAsync(string page, bool animated, IParametersService parameters = null)
        {
            await PushInternalAsync(page, animated, parameters);
        }

        public virtual async Task PushModalAsync(string page, IParametersService parameters = null)
        {
            await PushModalAsync(page, true, parameters);
        }

        public virtual async Task PushModalAsync(string page, bool animated, IParametersService parameters = null)
        {
            await PushInternalAsync(page, animated, parameters, true);
        }

        protected virtual async Task PushInternalAsync(string page, bool animated, IParametersService parameters = null, bool useModal = false)
        {
            var paramService = parameters ?? new ParametersService();
            var pageStack = useModal ? ModalStack :
                                       NavigationStack;
            if (pageStack.Count > 0)
            {
                var lastPage = pageStack.Last();
                PageCacheController.RemoveCachedPages(lastPage.Key);
            }
            var nextPage = PageCacheController.GetCachedOrNewPage(page, paramService);
            await NavigationController.PushPageAsync(page, nextPage, animated, paramService, useModal);
            PageCacheController.AddCachedPagesWithOption(page, CacheOption.Appears);
        }

        public virtual void RemovePage(string page)
        {
            NavigationController.RemovePage(page);
        }

        public virtual void SetMainPage(string page, IParametersService parameters = null)
        {
            //if (NavigationController.GetMainPage() != null && MainPageContainer != null)
            //{
            //    PageCacheController.RemoveCachedPages(MainPageContainer.Key);
            //}
            var paramService = parameters ?? new ParametersService();
            var nextPage = PageCacheController.GetCachedOrNewPage(page, paramService);
            var key = page;
            if (PageKeyParser.IsSequence(page))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(page);
                queue.Dequeue();
                key = queue.Dequeue();
            }
            NavigationController.SetMainPage(key, nextPage, paramService);
            PageCacheController.AddCachedPagesWithOption(page, CacheOption.Appears);
        }

        public virtual void InsertPageBefore<TClass, TBefore>(IParametersService parameters = null)
        {
            InsertPageBefore(typeof(TClass).Name, typeof(TBefore).Name, parameters);
        }

        public virtual Task PushAsync<TClass>(IParametersService parameters = null)
        {
            return PushAsync<TClass>(true, parameters);
        }

        public virtual Task PushAsync<TClass>(bool animated, IParametersService parameters = null)
        {
            return PushAsync(typeof(TClass).Name, animated, parameters);
        }

        public virtual Task PushModalAsync<TClass>(IParametersService parameters = null)
        {
            return PushModalAsync<TClass>(true, parameters);
        }

        public virtual Task PushModalAsync<TClass>(bool animated, IParametersService parameters = null)
        {
            return PushModalAsync(typeof(TClass).Name, animated, parameters);
        }

        public virtual void RemovePage<TClass>()
        {
            RemovePage(typeof(TClass).Name);
        }

        public virtual void SetMainPage<TClass>(IParametersService parameters = null)
        {
            SetMainPage(typeof(TClass).Name, parameters);
        }
    }
}
