﻿using System;
using System.Collections.Generic;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationStore
    {
        public static PageNavigationStore Current { get; } = new PageNavigationStore();

        public Dictionary<string, Type> PageTypes { get; } = new Dictionary<string, Type>();
    }
}
