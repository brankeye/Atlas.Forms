﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Atlas.Forms.Navigation
{
    public class PageNavigationStore
    {
        public static PageNavigationStore Current { get; set; } = new PageNavigationStore();

        public IDictionary<string, Type> PageTypes { get; } = new Dictionary<string, Type>();

        public IDictionary<string, ConstructorInfo> PageConstructors { get; } = new Dictionary<string, ConstructorInfo>();

        public void AddTypeAndConstructorInfo(string key, Type type)
        {
            PageTypes[key] = type;
            PageConstructors[key] = GetConstructor(type);
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
