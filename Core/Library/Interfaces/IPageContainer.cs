using System;

namespace atlas.core.Library.Interfaces
{
    public interface IPageContainer
    {
        string Key { get; set; }

        Type Type { get; set; }
    }
}
