using System;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class MessagingService : IMessagingService
    {
        public static IMessagingService Current => GetCurrent();
        private static Lazy<IMessagingService> _current;

        protected static IMessagingService GetCurrent()
        {
            if (_current == null)
            {
                _current = new Lazy<IMessagingService>();
            }
            return _current.Value;
        }

        public static void SetCurrent(Func<IMessagingService> func)
        {
            _current = new Lazy<IMessagingService>(func);
        }

        public virtual void SendMessage(string message)
        {
            MessagingCenter.Send(this, message);
        }

        public virtual void SendMessage<TArgs>(string message, TArgs args)
        {
            MessagingCenter.Send(this, message, args);
        }

        public virtual void Subscribe(string message, Action<IMessagingService> callback)
        {
            MessagingCenter.Subscribe(this, message, callback);
        }

        public virtual void Subscribe<TArgs>(string message, Action<IMessagingService, TArgs> callback)
        {
            MessagingCenter.Subscribe(this, message, callback);
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
