using System;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Interfaces.Utilities;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class MessagingService : IMessagingService
    {
        public static IMessagingService Current => Instance.Current;

        protected static ILazySingleton<IMessagingService> Instance { get; set; }
            = new LazySingleton<IMessagingService>();

        public static void SetCurrent(Func<IMessagingService> func)
        {
            Instance.SetCurrent(func);
        }

        public virtual void SendMessage(string message)
        {
            MessagingCenter.Send(this, message);
        }

        public virtual void SendMessage<TArgs>(string message, TArgs args)
        {
            MessagingCenter.Send(this, message, args);
        }

        public virtual void Subscribe(string message, Action callback)
        {
            Action<IMessagingService> action = service => callback.Invoke();
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Subscribe<TArgs>(string message, Action<TArgs> callback)
        {
            Action<IMessagingService, TArgs> action = (service, args) => callback.Invoke(args);
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Unsubscribe(string message)
        {
            MessagingCenter.Unsubscribe<IMessagingService>(this, message);
        }

        public virtual void Unsubscribe<TArgs>(string message)
        {
            MessagingCenter.Unsubscribe<IMessagingService, TArgs>(this, message);
        }
    }
}
