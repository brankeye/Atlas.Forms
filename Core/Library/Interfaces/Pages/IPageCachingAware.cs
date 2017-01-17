using Xamarin.Forms;

namespace atlas.core.Library.Interfaces.Pages
{
    public interface IPageCachingAware
    {
        void OnPageCaching(IParametersService parameters = null);
    }
}
