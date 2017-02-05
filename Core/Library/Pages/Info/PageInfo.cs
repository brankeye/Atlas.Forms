using System;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Pages.Info
{
    public class PageInfo : IPageInfo
    {
        public PageInfo() { }

        public PageInfo(string key, Type type)
        {
            Key = key;
            Type = type;
        }

        public PageInfo(IPageInfo info)
        {
            Key = info.Key;
            Type = info.Type;
        }

        public string Key { get; set; }

        public Type Type { get; set; }
    }
}
