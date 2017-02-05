using System;

namespace Atlas.Forms.Interfaces
{
    public interface IPageInfo
    {
        string Key { get; set; }

        Type Type { get; set; }
    }
}
