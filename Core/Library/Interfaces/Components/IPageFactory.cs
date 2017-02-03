namespace Atlas.Forms.Interfaces.Components
{
    public interface IPageFactory
    {
        object GetNewPage(string key, object pageArg = null);
    }
}
