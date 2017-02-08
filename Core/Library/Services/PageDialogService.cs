using System;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Interfaces.Utilities;
using Atlas.Forms.Navigation;
using Atlas.Forms.Utilities;

namespace Atlas.Forms.Services
{
    public class PageDialogService : IPageDialogService
    {
        public static IPageDialogService Current => Instance.Current;

        protected static ILazySingleton<IPageDialogService> Instance { get; set; }
            = new LazySingleton<IPageDialogService>(() => null);

        public static void SetCurrent(Func<IPageDialogService> func)
        {
            Instance.SetCurrent(func);
        }

        protected IApplicationProvider ApplicationProvider { get; }

        public PageDialogService()
        {
            ApplicationProvider = new ApplicationProvider();
        }

        public PageDialogService(IApplicationProvider applicationProvider)
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
