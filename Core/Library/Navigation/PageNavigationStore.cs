using System;
using System.Collections.Generic;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationStore
    {
        public static Dictionary<string, Type> PageTypes { get; } = new Dictionary<string, Type>();
    }
}
