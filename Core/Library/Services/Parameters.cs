using System;
using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Services
{
    public class ParametersService : IParametersService
    {
        public bool Add<TObject>(string key, TObject item)
        {
            throw new NotImplementedException();
        }

        public TObject Get<TObject>(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}
