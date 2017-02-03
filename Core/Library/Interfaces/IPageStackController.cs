using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Forms.Interfaces
{
    public interface IPageStackController
    {
        IReadOnlyList<IPageContainer> NavigationStack { get; }

        IReadOnlyList<IPageContainer> ModalStack { get; }

        IList<IPageContainer> CreateNavigationStack();

        IList<IPageContainer> CreateModalStack();
    }
}
