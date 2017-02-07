using System;

namespace Atlas.Forms.Utilities
{
    public class SuperLazySingleton<T> : LazySingleton<T>
        where T : new()
    {
        protected override Lazy<T> CreateCurrent()
        {
            return new Lazy<T>(() => new T());
        }
    }
}
