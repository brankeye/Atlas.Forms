using System.Threading.Tasks;
using Atlas.Forms.Interfaces;

namespace Atlas.Forms.Services
{
    public class DialogService : IDialogService
    {
        public static IDialogService Current { get; internal set; }

        protected IApplicationProvider ApplicationProvider { get; }

        public DialogService(IApplicationProvider applicationProvider)
        {
            ApplicationProvider = applicationProvider;
        }

        public virtual Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return ApplicationProvider.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public virtual Task DisplayAlert(string title, string message, string cancel)
        {
            return ApplicationProvider.MainPage.DisplayAlert(title, message, cancel);
        }

        public virtual Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return ApplicationProvider.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
