using System;
using atlas.core.Library.Interfaces;

namespace atlas.core.Library.Pages
{
    public class PageContainer : IPageContainer
    {
        public PageContainer(string key, Type pageType)
        {
            Key = key;
            PageType = pageType;
        }

        public string Key { get; set; }

        public Type PageType { get; set; }
    }
}
