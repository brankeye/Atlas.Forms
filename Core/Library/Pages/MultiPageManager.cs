using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Infos;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Managers;
using Atlas.Forms.Interfaces.Services;
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

        protected MultiPage<T> PageInstance { get; set; }

        protected IPageRetriever PageRetriever { get; set; }

        protected IPageKeyStore PageKeyStore { get; }

        public MultiPageManager(
            MultiPage<T> page,
            IPageRetriever pageRetriever,
            IPageKeyStore pageKeyStore)
        {
            PageInstance = page;
            PageRetriever = pageRetriever;
            PageKeyStore = pageKeyStore;
        }

        protected virtual IEnumerable GetItemsSource()
        {
            return PageInstance.ItemsSource;
        }

        protected virtual object GetSelectedItem()
        {
            return PageInstance.SelectedItem;
        }

        public void AddPage(NavigationInfo pageInfo, IParametersService parameters = null)
        {
            var pageInstance = PageRetriever.GetCachedOrNewPage(pageInfo, parameters ?? new ParametersService()) as T;
            PageInstance.Children.Add(pageInstance);
            var children = Children;
            if (children.Count == 1)
            {
                CurrentPage = children[0];
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
            var pageContainer = PageKeyStore.GetPageContainer(PageInstance.Children[index]);
            PageInstance.Children.RemoveAt(index);
            return pageContainer;
        }

        public IPageInfo SetCurrentPage(int index)
        {
            if (index < PageInstance.Children.Count)
            {
                var pageContainer = PageKeyStore.GetPageContainer(PageInstance.Children[index]);
                PageInstance.CurrentPage = PageInstance.Children[index];
                CurrentPage = pageContainer;
                return pageContainer;
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
            var pageInstance = PageRetriever.GetCachedOrNewPage(navigationInfo, new ParametersService());
            PageInstance.ItemTemplate = new DataTemplate(() => pageInstance);
        }

        public object SetSelectedItem(int index)
        {
            var itemsSource = PageInstance.ItemsSource as IList;
            if (index < itemsSource?.Count)
            {
                PageInstance.SelectedItem = itemsSource[index];
                return PageInstance.SelectedItem;
            }
            return null;
        }

        public IList<IPageInfo> CreateChildren()
        {
            var children = new List<IPageInfo>();
            foreach (var child in PageInstance.Children)
            {
                children.Add(PageKeyStore.GetPageContainer(child));
            }
            return children;
        }
    }
}
