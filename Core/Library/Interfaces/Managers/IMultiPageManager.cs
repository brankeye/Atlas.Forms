using System.Collections;
using System.Collections.Generic;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Services;

namespace Atlas.Forms.Interfaces.Managers
{
    public interface IMultiPageManager
    {
        IReadOnlyList<IPageInfo> Children { get; }

        IPageInfo CurrentPage { get; }

        object SelectedItem { get; }

        IEnumerable ItemsSource { get; }

        void AddPage(NavigationInfo pageInfo, IParametersService parameters = null);

        IPageInfo RemovePage(string page);

        IPageInfo RemovePageAt(int index);

        void SetPageTemplate(string page, IParametersService parameters = null);

        IPageInfo SetCurrentPage(string page);

        IPageInfo SetCurrentPage(int index);

        object SetSelectedItem(int index);
    }
}
