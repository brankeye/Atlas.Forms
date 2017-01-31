using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces.Pages
{
    public interface IPageAppearingAware
    {
        void OnPageAppearing(IParametersService parameters);
    }
}
