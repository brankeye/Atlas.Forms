using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces
{
    public interface INavigationServiceProvider
    {
        INavigationService NavigationService { get; set; }
    }
}
