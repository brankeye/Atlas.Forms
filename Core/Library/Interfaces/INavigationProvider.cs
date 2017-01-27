using Xamarin.Forms;

namespace Atlas.Forms.Interfaces
{
    public interface INavigationProvider
    {
        INavigation Navigation { get; set; }

        void TrySetNavigation(object pageArg);
    }
}
