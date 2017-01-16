using System;
using System.Collections.Generic;

namespace atlas.core.Library.Navigation
{
    public class PageNavigationStore
    {
        protected static Dictionary<string, Type> PageTypeStore { get; } = new Dictionary<string, Type>();

        public static bool TryAddType(string key, Type type)
        {
            if (PageTypeStore.ContainsKey(key) == false)
            {
                PageTypeStore.Add(key, type);
                return true;
            }
            return false;
        }

        public static Type GetPageType(string key)
        {
            return PageTypeStore[key];
        }
    }
}
