using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Infos;

namespace Atlas.Forms.Services
{
    public static class Nav
    {
        public static NavigationInfoFluent Get(string key)
        {
            return new NavigationInfoFluent(new NavigationInfo(key));
        }

        public static NavigationInfoFluent Get<TClass>()
        {
            return Get(typeof(TClass).Name);
        }
    }
}
