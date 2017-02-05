using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Navigation;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Pages
{
    public class MultiPageManager<T> : IMultiPageManager
        where T : Page
    {
        public object SelectedItem => Page.SelectedItem;

        public IEnumerable ItemsSource => Page.ItemsSource;

        public IPageContainer CurrentPage { get; protected set; }

        public IReadOnlyList<IPageContainer> Children => CreateChildren().ToList();

        protected MultiPage<T> Page { get; set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageCacheController PageCacheController { get; set; }

        public MultiPageManager(
            MultiPage<T> page,
            INavigationController navigationController,
            IPageCacheController pageCacheController)
        {
            Page = page;
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public void AddPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var pageInstance = PageCacheController.GetCachedOrNewPage(pageInfo, parameters ?? new ParametersService()) as T;
            Page.Children.Add(pageInstance);
            PageCacheController.AddCachedPagesWithOption(pageInfo.Page, CacheOption.Appears);
            var children = Children;
            if (children.Count == 1)
            {
                CurrentPage = children[0];
            }
        }

        public IPageContainer RemovePage(string page)
        {
            var children = Children;
            for (var i = 0; i < children.Count; i++)
            {
                var container = children[i];
                if (container.Key == page)
                {
                    return RemovePageAt(i);
                }
            }
            return null;
        }

        public IPageContainer RemovePageAt(int index)
        {
            var pageContainer = PageKeyStore.Current.GetPageContainer(Page.Children[index]);
            Page.Children.RemoveAt(index);
            return pageContainer;
        }

        public IPageContainer SetCurrentPage(int index)
        {
            if (index < Page.Children.Count)
            {
                var pageContainer = PageKeyStore.Current.GetPageContainer(Page.Children[index]);
                Page.CurrentPage = Page.Children[index];
                CurrentPage = pageContainer;
                return pageContainer;
            }
            return null;
        }

        public IPageContainer SetCurrentPage(string page)
        {
            var children = Children;
            for (var i = 0; i < children.Count; i++)
            {
                var container = children[i];
                if (container.Key == page)
                {
                    return SetCurrentPage(i);
                }
            }
            return null;
        }

        public void SetPageTemplate(string page, IParametersService parameters = null)
        {
            var pageInstance = PageCacheController.TryGetCachedPage(page, parameters ?? new ParametersService());
            Page.ItemTemplate = new DataTemplate(() => pageInstance); 
        }

        public object SetSelectedItem(int index)
        {
            var itemsSource = Page.ItemsSource as IList;
            if (index < itemsSource?.Count)
            {
                Page.SelectedItem = itemsSource[index];
                return Page.SelectedItem;
            }
            return null;
        }

        public IList<IPageContainer> CreateChildren()
        {
            var children = new List<IPageContainer>();
            foreach (var child in Page.Children)
            {
                children.Add(PageKeyStore.Current.GetPageContainer(child));
            }
            return children;
        }
    }
}
