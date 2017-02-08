using System.Collections.Generic;
using System.Linq;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Navigation;

namespace Atlas.Forms.Components
{
    public class PageStackController : IPageStackController
    {
        public IReadOnlyList<IPageInfo> NavigationStack => CreateNavigationStack().ToList();

        public IReadOnlyList<IPageInfo> ModalStack => CreateModalStack().ToList();

        protected INavigationProvider NavigationProvider { get; }

        protected IPageKeyStore PageKeyStore { get; }

        public PageStackController(INavigationProvider navigationProvider, IPageKeyStore pageKeyStore)
        {
            NavigationProvider = navigationProvider;
            PageKeyStore = pageKeyStore;
        }

        public virtual IList<IPageInfo> CreateNavigationStack()
        {
            return CreateStack(false);
        }

        public virtual IList<IPageInfo> CreateModalStack()
        {
            return CreateStack(true);
        }

        protected virtual IList<IPageInfo> CreateStack(bool isModal)
        {
            var currentStack = isModal ? NavigationProvider.Navigation.ModalStack 
                                   : NavigationProvider.Navigation.NavigationStack;
            if (currentStack != null)
            {
                var stack = PageKeyStore.GetPageContainers(currentStack.ToList());
                return stack;
            }
            return new List<IPageInfo>();
        }
    }
}
