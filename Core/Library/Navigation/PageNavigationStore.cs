using System;
using System.Collections.Generic;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationStore
    {
        public static PageNavigationStore Current { get; set; } = new PageNavigationStore();

        public IDictionary<string, Type> PageTypes { get; } = new Dictionary<string, Type>();
    }
}
