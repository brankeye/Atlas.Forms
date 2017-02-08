using System;
using Atlas.Forms.Interfaces.Utilities;

namespace Atlas.Forms.Utilities
{
    public class LazySingleton<T> : ILazySingleton<T>
    {
        public T Current => GetCurrent();
        private Lazy<T> _current;

        public LazySingleton(Func<T> func)
        {
            SetCurrent(func);
        }

        protected virtual T GetCurrent()
        {
            return _current.Value;
        }

        public void SetCurrent(Func<T> func)
        {
            _current = new Lazy<T>(func);
        }
    }
}
