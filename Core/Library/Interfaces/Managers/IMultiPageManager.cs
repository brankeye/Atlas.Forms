using System.Collections;
using System.Collections.Generic;
using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces.Managers
{
    public interface IMultiPageManager
    {
        IReadOnlyList<IPageContainer> Children { get; }

        IPageContainer CurrentPage { get; }

        object SelectedItem { get; }

        IEnumerable ItemsSource { get; }

        void AddPage(string page, IParametersService parameters = null);

        IPageContainer RemovePage(string page);

        IPageContainer RemovePageAt(int index);

        void SetPageTemplate(string page, IParametersService parameters = null);

        IPageContainer SetCurrentPage(string page);

        IPageContainer SetCurrentPage(int index);

        object SetSelectedItem(int index);
    }
}
