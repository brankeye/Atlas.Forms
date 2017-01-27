using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface INavigationService
    {
        IReadOnlyList<IPageContainer> ModalStack { get; }

        IReadOnlyList<IPageContainer> NavigationStack { get; }

        INavigationProvider NavigationProvider { get; set; }

        void InsertPageBefore(string page, string before, IParametersService parameters = null);

        Task<IPageContainer> PopAsync(IParametersService parameters = null);

        Task<IPageContainer> PopAsync(bool animated, IParametersService parameters = null);

        Task<IPageContainer> PopModalAsync(IParametersService parameters = null);

        Task<IPageContainer> PopModalAsync(bool animated, IParametersService parameters = null);

        Task PopToRootAsync();

        Task PopToRootAsync(bool animated);

        Task PushAsync(string page, IParametersService parameters = null);

        Task PushAsync(string page, bool animated, IParametersService parameters = null);

        Task PushModalAsync(string page, IParametersService parameters = null);

        Task PushModalAsync(string page, bool animated, IParametersService parameters = null);

        void RemovePage(string page);

        void SetMainPage(string page, IParametersService parameters = null);
    }
}
