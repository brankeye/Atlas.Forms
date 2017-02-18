using System;
using System.Threading.Tasks;

// Xamarin.Forms.Toolkit
// Source: https://github.com/jamesmontemagno/xamarin.forms-toolkit/blob/master/FormsToolkit/FormsToolkit/Interfaces/IMessagingService.cs

namespace Atlas.Forms.Interfaces.Services
{
    public interface IMessagingService
    {
        void Subscribe(string message, Action callback);

        void Subscribe<T>(string message, Action<T> callback);
        
        void Subscribe<T1, T2>(string message, Action<T1, T2> callback);
        
        void Subscribe<T1, T2, T3>(string message, Action<T1, T2, T3> callback);
        
        void Subscribe<T1, T2, T3, T4>(string message, Action<T1, T2, T3, T4> callback);

        void SubscribeAsync(string message, Func<Task> callback);

        void SubscribeAsync<T>(string message, Func<T, Task> callback);

        void SubscribeAsync<T1, T2>(string message, Func<T1, T2, Task> callback);

        void SubscribeAsync<T1, T2, T3>(string message, Func<T1, T2, T3, Task> callback);

        void SubscribeAsync<T1, T2, T3, T4>(string message, Func<T1, T2, T3, T4, Task> callback);

        void SendMessage(string message);

        void SendMessage<T>(string message, T args);

        void SendMessage<T1, T2>(string message, T1 arg1, T2 arg2);

        void SendMessage<T1, T2, T3>(string message, T1 arg1, T2 arg2, T3 arg3);

        void SendMessage<T1, T2, T3, T4>(string message, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        void Unsubscribe(string message);

        void Unsubscribe<T>(string message);

        void Unsubscribe<T1, T2>(string message);

        void Unsubscribe<T1, T2, T3>(string message);

        void Unsubscribe<T1, T2, T3, T4>(string message);
    }
}
