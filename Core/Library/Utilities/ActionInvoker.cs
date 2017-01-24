using System;

namespace Atlas.Forms.Utilities
{
    public class ActionInvoker
    {
        public static void Invoke<T>(object item, Action<T> action)
            where T : class
        {
            var convertedItem = item as T;
            if (convertedItem != null)
            {
                action.Invoke(convertedItem);
            }
        }
    }
}
