using System;
using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Pages
{
    public class PageContainer : IPageContainer
    {
        public PageContainer(string key, Type type)
        {
            Key = key;
            Type = type;
        }

        public string Key { get; set; }

        public Type Type { get; set; }
    }
}
