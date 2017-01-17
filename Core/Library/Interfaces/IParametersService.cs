namespace atlas.core.Library.Interfaces
{
    public interface IParametersService
    {
        bool TryAdd(string key, object item);

        TObject TryGet<TObject>(string key);

        bool TryRemove(string key);
    }
}
