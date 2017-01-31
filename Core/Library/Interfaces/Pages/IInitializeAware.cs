using Atlas.Forms.Interfaces.Services;

namespace Atlas.Forms.Interfaces.Pages
{
    public interface IInitializeAware
    {
        void Initialize(IParametersService parameters);
    }
}
