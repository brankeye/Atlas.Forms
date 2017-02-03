namespace Atlas.Forms.Interfaces.Api
{
    public interface INavigationApi
    {
        INavigationApi AsNavigationPage();

        INavigationApi As(string key);

        INavigationApi As<TClass>();

        INavigationApi AsNewInstance();
    }
}
