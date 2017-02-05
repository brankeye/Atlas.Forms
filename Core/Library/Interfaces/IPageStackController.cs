using System.Collections.Generic;

namespace Atlas.Forms.Interfaces
{
    public interface IPageStackController
    {
        IReadOnlyList<IPageInfo> NavigationStack { get; }

        IReadOnlyList<IPageInfo> ModalStack { get; }

        IList<IPageInfo> CreateNavigationStack();

        IList<IPageInfo> CreateModalStack();
    }
}
