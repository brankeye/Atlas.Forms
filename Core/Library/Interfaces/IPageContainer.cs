using System;

namespace atlas.core.Library.Interfaces
{
    public interface IPageContainer
    {
        string Key { get; set; }

        Type PageType { get; set; }
    }
}
