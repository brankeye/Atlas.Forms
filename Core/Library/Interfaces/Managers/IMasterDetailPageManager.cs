using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces.Managers
{
    public interface IMasterDetailPageManager
    {
        void PresentPage(string page, IParametersService parameters = null);
    }
}
