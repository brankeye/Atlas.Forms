using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages;

namespace Atlas.Forms.Interfaces.Managers
{
    public interface IMasterDetailPageManager
    {
        void PresentPage(NavigationInfo pageInfo, IParametersService parameters = null);
    }
}
