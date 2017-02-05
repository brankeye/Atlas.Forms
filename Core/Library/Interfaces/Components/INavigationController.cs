using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface INavigationController
    {
        void SetMainPage(object page, IParametersService parameters = null);

        IReadOnlyList<IPageInfo> GetNavigationStack();

        IReadOnlyList<IPageInfo> GetModalStack();

        INavigation GetNavigation();

        Task PushPageAsync(object page, bool animated, IParametersService parameters, bool useModal);

        void InsertPageBefore(object page, string before, IParametersService parameters);

        Task<IPageInfo> PopPageAsync(bool animated, IParametersService parameters, bool useModal);

        Task PopToRootAsync(bool animated, IParametersService parameters);

        void RemovePage(string pageKey);

        void TrySetNavigation(object page);
    }
}
