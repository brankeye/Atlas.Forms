using System;

namespace Atlas.Forms.Interfaces
{
    public interface IPageContainer
    {
        string Key { get; set; }

        Type Type { get; set; }
    }
}
