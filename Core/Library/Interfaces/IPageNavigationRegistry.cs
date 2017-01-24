namespace Atlas.Forms.Interfaces
{
    public interface IPageNavigationRegistry
    {
        void RegisterPage<TPage>();

        void RegisterPage<TPage>(string key);
    }
}
