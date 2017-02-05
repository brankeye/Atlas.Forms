using System;

namespace Atlas.Forms.Interfaces
{
    public interface IServiceFactory
    {
        T CreateService<T>(params object[] args) where T : class;

        bool AddService(Type type, Func<object[], object> service);

        void Lock();
    }
}
