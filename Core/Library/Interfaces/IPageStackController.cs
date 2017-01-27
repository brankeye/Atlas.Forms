using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Forms.Interfaces
{
    public interface IPageStackController
    {
        IList<IPageContainer> NavigationStack { get; }

        IList<IPageContainer> ModalStack { get; }

        void AddPageToNavigationStack(string pageKey);

        void AddPageToModalStack(string pageKey);
    }
}
