using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces.Pages
{
    public interface IPageDisappearedAware
    {
        void OnPageDisappeared(IParametersService parameters);
    }
}
