using System;
using System.Collections.Generic;

namespace atlas.core.Library.Navigation
{
    public static class PageNavigationStore
    {
        public static Dictionary<string, Type> PageTypeStore { get; } = new Dictionary<string, Type>();

        public static Type GetPageType(string key)
        {
            return PageTypeStore[key];
        }
    }
}
