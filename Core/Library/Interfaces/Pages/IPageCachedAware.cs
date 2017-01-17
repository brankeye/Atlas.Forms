namespace atlas.core.Library.Interfaces.Pages
{
    public interface IPageCachedAware
    {
        void OnPageCached(IParametersService parameters = null);
    }
}
