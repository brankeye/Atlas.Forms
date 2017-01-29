using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Enums;
using Atlas.Forms.Pages.Containers;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IPageCacheController
    {
        object GetCachedOrNewPage(string key, IParametersService parameters);

        void RemoveCachedPages(string key);

        void AddCachedPages(string key);

        void AddCachedPagesWithOption(string key, CacheOption cacheOption);
    }
}
