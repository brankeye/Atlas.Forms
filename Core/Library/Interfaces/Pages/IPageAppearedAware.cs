using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces.Pages
{
    public interface IPageAppearedAware
    {
        void OnPageAppeared(IParametersService parameters);
    }
}
