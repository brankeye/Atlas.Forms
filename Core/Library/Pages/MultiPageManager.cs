using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public object SelectedItem => GetSelectedItem();

        public IEnumerable ItemsSource => GetItemsSource();

        public IPageInfo CurrentPage { get; protected set; }

        public IReadOnlyList<IPageInfo> Children => CreateChildren().ToList();

        protected WeakReference<MultiPage<T>> PageReference { get; set; }

        protected INavigationController NavigationController { get; set; }

        protected IPageRetriever PageRetriever { get; set; }

        protected IPageKeyStore PageKeyStore { get; }

        public MultiPageManager(
            MultiPage<T> page,
            INavigationController navigationController,
            IPageRetriever pageRetriever,
            IPageKeyStore pageKeyStore)
        {
            PageReference = new WeakReference<MultiPage<T>>(page);
            NavigationController = navigationController;
            PageRetriever = pageRetriever;
            PageKeyStore = pageKeyStore;
        }

        protected virtual IEnumerable GetItemsSource()
        {
            MultiPage<T> page;
            if (PageReference.TryGetTarget(out page))
            {
                return page.ItemsSource;
            }
            return null;
        }

        protected virtual object GetSelectedItem()
        {
            MultiPage<T> page;
            if (PageReference.TryGetTarget(out page))
            {
                return page.SelectedItem;
            }
            return null;
        }

        public void AddPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var pageInstance = PageRetriever.GetCachedOrNewPage(pageInfo, parameters ?? new ParametersService()) as T;
            MultiPage<T> page;
            if (PageReference.TryGetTarget(out page))
            {
                page.Children.Add(pageInstance);
                var children = Children;
                if (children.Count == 1)
                {
                    CurrentPage = children[0];
                }
            }
            
        }

        public IPageInfo RemovePage(NavigationInfo navigationInfo)
        {
            var children = Children;
            for (var i = 0; i < children.Count; i++)
            {
                var container = children[i];
                if (container.Key == navigationInfo.Page)
                {
                    return RemovePageAt(i);
                }
            }
            return null;
        }

        public IPageInfo RemovePageAt(int index)
        {
            MultiPage<T> page;
            if (PageReference.TryGetTarget(out page))
            {
                var pageContainer = PageKeyStore.GetPageContainer(page.Children[index]);
                page.Children.RemoveAt(index);
                return pageContainer;
            }
            return null;
        }

        public IPageInfo SetCurrentPage(int index)
        {
            MultiPage<T> page;
            if (PageReference.TryGetTarget(out page))
            {
                if (index < page.Children.Count)
                {
                    var pageContainer = PageKeyStore.GetPageContainer(page.Children[index]);
                    page.CurrentPage = page.Children[index];
                    CurrentPage = pageContainer;
                    return pageContainer;
                }
            }
            
            return null;
        }

        public IPageInfo SetCurrentPage(NavigationInfo navigationInfo)
        {
            var children = Children;
            for (var i = 0; i < children.Count; i++)
            {
                var container = children[i];
                if (container.Key == navigationInfo.Page)
                {
                    return SetCurrentPage(i);
                }
            }
            return null;
        }

        public void SetPageTemplate(NavigationInfo navigationInfo)
        {
            MultiPage<T> pageRef;
            if (PageReference.TryGetTarget(out pageRef))
            {
                var pageInstance = PageRetriever.GetCachedOrNewPage(navigationInfo, new ParametersService());
                pageRef.ItemTemplate = new DataTemplate(() => pageInstance);
            }
        }

        public object SetSelectedItem(int index)
        {
            MultiPage<T> pageRef;
            if (PageReference.TryGetTarget(out pageRef))
            {
                var itemsSource = pageRef.ItemsSource as IList;
                if (index < itemsSource?.Count)
                {
                    pageRef.SelectedItem = itemsSource[index];
                    return pageRef.SelectedItem;
                }
            }
            return null;
        }

        public IList<IPageInfo> CreateChildren()
        {
            MultiPage<T> pageRef;
            if (PageReference.TryGetTarget(out pageRef))
            {
            }
            var children = new List<IPageInfo>();
            foreach (var child in pageRef.Children)
            {
                children.Add(PageKeyStore.GetPageContainer(child));
            }
            return children;
        }
    }
}
