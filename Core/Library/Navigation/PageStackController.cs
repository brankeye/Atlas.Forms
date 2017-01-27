using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Navigation
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
