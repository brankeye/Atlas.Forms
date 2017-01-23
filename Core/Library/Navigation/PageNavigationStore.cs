using System;
using System.Collections.Generic;

namespace atlas.core.Library.Navigation
{
    public class PageNavigationStore
    {
        public static Dictionary<string, Type> PageTypes { get; } = new Dictionary<string, Type>();
    }
}
