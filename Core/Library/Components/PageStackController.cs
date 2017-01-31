using System;
using System.Collections.Generic;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Components
{
    public class PageStackController : IPageStackController
    {
        public IList<IPageContainer> NavigationStack { get; } = new List<IPageContainer>();

        public IList<IPageContainer> ModalStack { get; } = new List<IPageContainer>();

        public virtual void AddPageToNavigationStack(string pageKey)
        {
            AddPageToStack(pageKey);
        }

        public virtual void AddPageToModalStack(string pageKey)
        {
            AddPageToStack(pageKey, true);
        }

        public virtual IPageContainer PopPageFromNavigationStack()
        {
            if (NavigationStack.Count > 0)
            {
                var pageContainer = NavigationStack[NavigationStack.Count - 1];
                NavigationStack.RemoveAt(NavigationStack.Count - 1);
                return pageContainer;
            }
            return null;
        }

        public virtual IPageContainer PopPageFromModalStack()
        {
            if (ModalStack.Count > 0)
            {
                var pageContainer = ModalStack[ModalStack.Count - 1];
                ModalStack.RemoveAt(ModalStack.Count - 1);
                return pageContainer;
            }
            return null;
        }

        protected virtual void AddPageToStack(string pageKey, bool useModal = false)
        {
            PageContainer container;
            if (PageKeyParser.IsSequence(pageKey))
            {
                var queue = PageKeyParser.GetPageKeysFromSequence(pageKey);
                queue.Dequeue();
                var innerPageKey = queue.Dequeue();
                Type innerPageType;
                PageNavigationStore.Current.PageTypes.TryGetValue(innerPageKey, out innerPageType);
                if (innerPageType == null)
                {
                    return;
                }
                container = new PageContainer(innerPageKey, innerPageType);
            }
            else
            {
                container = new PageContainer(pageKey, PageNavigationStore.Current.PageTypes[pageKey]);
            }

            if (useModal)
            {
                ModalStack.Add(container);
            }
            else
            {
                NavigationStack.Add(container);
            }
        }
    }
}
