﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Forms.Utilities
{
    public class PropertyInjector
    {
        public static void Inject<TSource, TInjectable>(object source, TInjectable injectable, Action<TSource, TInjectable> action)
            where TSource : class
            where TInjectable : class
        {
            var item = source as TSource;
            if (item != null)
            {
                action.Invoke(item, injectable);
            }
        }
    }
}
