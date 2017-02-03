using System;
using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;
using Atlas.Forms.Pages;
using Atlas.Forms.Pages.Containers;

namespace Atlas.Forms.Components
{
    public class PageStackController : IPageStackController
    {
        public IReadOnlyList<IPageContainer> NavigationStack => CreateNavigationStack().ToList();

        public IReadOnlyList<IPageContainer> ModalStack => CreateModalStack().ToList();

        protected INavigationProvider NavigationProvider { get; }

        public PageStackController(INavigationProvider navigationProvider)
        {
            NavigationProvider = navigationProvider;
        }

        public virtual IList<IPageContainer> CreateNavigationStack()
        {
            return CreateStack(false);
        }

        public virtual IList<IPageContainer> CreateModalStack()
        {
            return CreateStack(true);
        }

        protected virtual IList<IPageContainer> CreateStack(bool isModal)
        {
            var currentStack = isModal ? NavigationProvider.Navigation.ModalStack 
                                   : NavigationProvider.Navigation.NavigationStack;
            var pageStack = new List<IPageContainer>();
            if (currentStack != null)
            {
                foreach (var page in currentStack)
                {
                    pageStack.Add(PageNavigationStore.Current.GetPageContainer(page));
                }
            }
            return pageStack;
        }
    }
}
