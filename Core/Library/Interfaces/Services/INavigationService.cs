using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas.Forms.Infos;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Services
{
    public interface INavigationService
    {
        IReadOnlyList<IPageInfo> ModalStack { get; }

        IReadOnlyList<IPageInfo> NavigationStack { get; }

        INavigation Navigation { get; }

        void InsertPageBefore(NavigationInfo pageInfo, NavigationInfo before, IParametersService parameters = null);

        Task<IPageInfo> PopAsync(IParametersService parameters = null);

        Task<IPageInfo> PopAsync(bool animated, IParametersService parameters = null);

        Task<IPageInfo> PopModalAsync(IParametersService parameters = null);

        Task<IPageInfo> PopModalAsync(bool animated, IParametersService parameters = null);

        Task PopToRootAsync();

        Task PopToRootAsync(bool animated);

        Task PushAsync(NavigationInfo pageInfo, IParametersService parameters = null);

        Task PushAsync(NavigationInfo pageInfo, bool animated, IParametersService parameters = null);

        Task PushModalAsync(NavigationInfo pageInfo, IParametersService parameters = null);

        Task PushModalAsync(NavigationInfo pageInfo, bool animated, IParametersService parameters = null);

        void RemovePage(NavigationInfo pageInfo);

        void SetMainPage(NavigationInfo pageInfo, IParametersService parameters = null);
    }
}
