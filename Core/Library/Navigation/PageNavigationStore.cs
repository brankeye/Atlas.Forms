using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Atlas.Forms.Interfaces;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationStore : IPageNavigationStore
    {
        public IDictionary<string, ConstructorInfo> PageConstructors { get; } = new Dictionary<string, ConstructorInfo>();

        public Type GetPageType(string key)
        {
            ConstructorInfo constructorInfo;
            PageConstructors.TryGetValue(key, out constructorInfo);
            return constructorInfo?.DeclaringType;
        }

        public void AddTypeAndConstructorInfo(string key, Type type)
        {
            PageConstructors[key] = GetConstructor(type);
        }

        public ConstructorInfo GetConstructor(string key)
        {
            return GetConstructor(GetPageType(key));
        }

        public ConstructorInfo GetConstructor(Type type)
        {
            var declaredConstructors = type.GetTypeInfo().DeclaredConstructors.Where(x => x.IsPublic);
            ConstructorInfo defaultConstructor = null;
            ConstructorInfo pageParamConstructor = null;
            foreach (var constructor in declaredConstructors)
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    defaultConstructor = constructor;
                }
                else if (parameters.Length == 1)
                {
                    var parameterInfo = parameters.FirstOrDefault();
                    if (parameterInfo.ParameterType == typeof(Page))
                    {
                        pageParamConstructor = constructor;
                    }
                }
            }

            return pageParamConstructor ?? defaultConstructor;
        }
    }
}
