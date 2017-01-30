using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
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

        public IReadOnlyList<IPageContainer> Children => ChildrenInternal.ToList();

        protected MultiPage<T> Page { get; set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageCacheController PageCacheController { get; set; }

        protected IList<IPageContainer> ChildrenInternal { get; set; } = new List<IPageContainer>();

        public MultiPageManager(
            MultiPage<T> page,
            INavigationController navigationController,
            IPageCacheController pageCacheController)
        {
            Page = page;
            NavigationController = navigationController;
            PageCacheController = pageCacheController;
        }

        public void AddPage(string page, IParametersService parameters = null)
        {
            var pageInstance = PageCacheController.GetCachedOrNewPage(page, parameters ?? new ParametersService());
            NavigationController.TrySetNavigation(pageInstance);
            Page.Children.Add(pageInstance as T);
            NavigationController.AddPageToNavigationStack(page);
            PageCacheController.AddCachedPagesWithOption(page, CacheOption.Appears);
            if (ChildrenInternal.Count == 1)
            {
                CurrentPage = ChildrenInternal[0];
            }
        }

        public IPageContainer RemovePage(string page)
        {
            for (var i = 0; i < ChildrenInternal.Count; i++)
            {
                var container = ChildrenInternal[i];
                if (container.Key == page)
                {
                    return RemovePageAt(i);
                }
            }
            return null;
        }

        public IPageContainer RemovePageAt(int index)
        {
            if (index < ChildrenInternal.Count)
            {
                var container = ChildrenInternal[index];
                ChildrenInternal.RemoveAt(index);
                Page.Children.RemoveAt(index);
                return container;
            }
            return null;
        }

        public IPageContainer SetCurrentPage(int index)
        {
            if (index < ChildrenInternal.Count)
            {
                var container = ChildrenInternal[index];
                Page.CurrentPage = Page.Children[index];
                CurrentPage = container;
            }
            return null;
        }

        public IPageContainer SetCurrentPage(string page)
        {
            for (var i = 0; i < ChildrenInternal.Count; i++)
            {
                var container = ChildrenInternal[i];
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
    }
}
