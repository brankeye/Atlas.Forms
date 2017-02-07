using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface IPageFactory
    {
        Page GetNewPage(string key, Page pageArg = null);
    }
}
