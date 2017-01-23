using System;
using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Pages.Containers
{
    public class PageContainer : IPageContainer
    {
        public PageContainer() { }

        public PageContainer(string key, Type type)
        {
            Key = key;
            Type = type;
        }

        public PageContainer(IPageContainer container)
        {
            Key = container.Key;
            Type = container.Type;
        }

        public string Key { get; set; }

        public Type Type { get; set; }
    }
}
