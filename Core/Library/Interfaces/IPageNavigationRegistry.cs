namespace atlas.core.Library.Interfaces
{
    public interface IPageNavigationRegistry
    {
        void RegisterPage<TPage>();

        void RegisterPage<TPage>(string key);
    }
}
