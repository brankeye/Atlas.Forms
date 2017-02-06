using System;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Services
{
    public class MessagingService : IMessagingService
    {
        public static IMessagingService Current
        {
            get { return _current ?? (_current = new MessagingService()); }
            internal set { _current = value; }
        }
        private static IMessagingService _current;

        public void SendMessage(string message)
        {
            MessagingCenter.Send(this, message);
        }

        public void SendMessage<TArgs>(string message, TArgs args)
        {
            MessagingCenter.Send(this, message, args);
        }

        public void Subscribe(string message, Action<IMessagingService> callback)
        {
            MessagingCenter.Subscribe(this, message, callback);
        }

        public void Subscribe<TArgs>(string message, Action<IMessagingService, TArgs> callback)
        {
            MessagingCenter.Subscribe(this, message, callback);
        }

        public void Unsubscribe(string message)
        {
            MessagingCenter.Unsubscribe<IMessagingService>(this, message);
        }

        public void Unsubscribe<TArgs>(string message)
        {
            MessagingCenter.Unsubscribe<IMessagingService, TArgs>(this, message);
        }
    }
}
