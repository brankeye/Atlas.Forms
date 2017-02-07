using System.Collections.Generic;
using Atlas.Forms.Enums;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Pages.Infos;
using Atlas.Forms.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IPageRetriever
    {
        Page GetCachedOrNewPage(NavigationInfo pageInfo, IParametersService parameters);

        Page TryGetCachedPage(string key, IParametersService parameters);

        Page GetNewPage(NavigationInfo pageInfo);
    }
}
