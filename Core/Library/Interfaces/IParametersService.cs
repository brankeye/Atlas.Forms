namespace atlas.core.Library.Interfaces
{
    public interface IParametersService
    {
        bool Add<TObject>(string key, TObject item);

        TObject Get<TObject>(string key);

        bool Remove(string key);
    }
}
