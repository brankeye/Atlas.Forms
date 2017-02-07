using System;

namespace Atlas.Forms.Interfaces.Utilities
{
    public interface ILazySingleton<T>
    {
        T Current { get; }

        void SetCurrent(Func<T> func);
    }
}
