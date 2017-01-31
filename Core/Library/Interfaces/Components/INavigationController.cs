using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface INavigationController
    {
        void SetMainPage(string pageKey, object page, IParametersService parameters = null);

        object GetMainPage();

        IReadOnlyList<IPageContainer> GetNavigationStack();

        IReadOnlyList<IPageContainer> GetModalStack();

        INavigation GetNavigation();

        Task PushPageAsync(string pageKey, object page, bool animated, IParametersService parameters, bool useModal);

        void InsertPageBefore(string pageKey, object page, string before, IParametersService parameters);

        Task<IPageContainer> PopPageAsync(bool animated, IParametersService parameters, bool useModal);

        Task PopToRootAsync(bool animated, IParametersService parameters);

        void RemovePage(string page);

        void TrySetNavigation(object page);

        void AddPageToNavigationStack(string page);
    }
}
