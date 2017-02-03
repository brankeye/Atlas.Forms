using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Api;

namespace Atlas.Forms.Navigation
{
    public class NavigationApi : INavigationApi
    {
        public NavigationApi()
        {
            
        }

        public INavigationApi As(string key)
        {
            throw new NotImplementedException();
        }

        public INavigationApi As<TClass>()
        {
            throw new NotImplementedException();
        }

        public INavigationApi AsNavigationPage()
        {
            throw new NotImplementedException();
        }

        public INavigationApi AsNewInstance()
        {
            throw new NotImplementedException();
        }
    }
}
