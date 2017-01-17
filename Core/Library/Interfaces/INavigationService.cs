using System.Collections.Generic;
using System.Threading.Tasks;

namespace atlas.core.Library.Interfaces
{
    public interface INavigationService
    {
        IReadOnlyList<IPageContainer> ModalStack { get; }

        IReadOnlyList<IPageContainer> NavigationStack { get; }

        void InsertPageBefore(string page, string before);

        Task<IPageContainer> PopAsync();

        Task<IPageContainer> PopAsync(bool animated);

        Task<IPageContainer> PopModalAsync();

        Task<IPageContainer> PopModalAsync(bool animated);

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
