using System;
using System.Reflection;

namespace Atlas.Forms.Interfaces
{
    public interface IPageNavigationStore
    {
        Type GetPageType(string key);

        void AddTypeAndConstructorInfo(string key, Type type);

        ConstructorInfo GetConstructor(Type type);

        ConstructorInfo GetConstructor(string key);
    }
}
