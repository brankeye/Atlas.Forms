using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces.Pages
{
    public interface IPageDisappearingAware
    {
        void OnPageDisappearing(IParametersService parameters);
    }
}
