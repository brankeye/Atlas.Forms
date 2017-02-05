using System;
using System.Collections.Generic;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Components;

namespace Atlas.Forms.Utilities
{
    public class ServiceFactory : IServiceFactory
    {
        protected IDictionary<object, Func<object[], object>> Services { get; set; } = new Dictionary<object, Func<object[], object>>();

        private bool Locker { get; set; }

        public virtual T CreateService<T>(params object[] args) where T : class
        {
            Func<object[], object> service;
            Services.TryGetValue(typeof(T), out service);
            return service?.Invoke(args) as T;
        }

        public virtual void AddService(Type type, Func<object[], object> service)
        {
            if (!Locker)
            {
                Services.Add(type, service);
            }
        }

        public virtual void Lock()
        {
            Locker = true;
        }
    }
}
