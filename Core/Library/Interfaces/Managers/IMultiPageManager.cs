using System.Collections;
using System.Collections.Generic;
using Atlas.Forms.Infos;
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

        IPageInfo RemovePage(NavigationInfo navigationInfo);

        IPageInfo RemovePageAt(int index);

        void SetPageTemplate(NavigationInfo navigationInfo);

        IPageInfo SetCurrentPage(NavigationInfo navigationInfo);

        IPageInfo SetCurrentPage(int index);

        object SetSelectedItem(int index);
    }
}
