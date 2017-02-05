using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IServiceFactory
    {
        T CreateService<T>(params object[] args) where T : class;

        void AddService(Type type, Func<object[], object> service);
    }
}
