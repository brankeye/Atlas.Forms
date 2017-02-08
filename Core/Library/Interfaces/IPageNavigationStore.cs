using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
