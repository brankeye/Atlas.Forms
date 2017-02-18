using System;
using System.Threading.Tasks;
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
            = new LazySingleton<IMessagingService>(() => new MessagingService());

        public static void SetCurrent(Func<IMessagingService> func)
        {
            Instance.SetCurrent(func);
        }

        public virtual void SendMessage(string message)
        {
            MessagingCenter.Send(this, message);
        }

        public virtual void SendMessage<T>(string message, T arg)
        {
            MessagingCenter.Send(this, message, arg);
        }

        public virtual void SendMessage<T1, T2>(string message, T1 arg1, T2 arg2)
        {
            MessagingCenter.Send(this, message, new Tuple<T1, T2>(arg1, arg2));
        }

        public virtual void SendMessage<T1, T2, T3>(string message, T1 arg1, T2 arg2, T3 arg3)
        {
            MessagingCenter.Send(this, message, new Tuple<T1, T2, T3>(arg1, arg2, arg3));
        }

        public virtual void SendMessage<T1, T2, T3, T4>(string message, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            MessagingCenter.Send(this, message, new Tuple<T1, T2, T3, T4>(arg1, arg2, arg3, arg4));
        }

        public virtual void SendMessage<T1, T2, T3, T4, T5>(string message, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            MessagingCenter.Send(this, message, new Tuple<T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5));
        }

        public virtual void SendMessage<T1, T2, T3, T4, T5, T6>(string message, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            MessagingCenter.Send(this, message, new Tuple<T1, T2, T3, T4, T5, T6>(arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public virtual void Subscribe(string message, Action callback)
        {
            Action<MessagingService> action = service => callback.Invoke();
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Subscribe<T>(string message, Action<T> callback)
        {
            Action<MessagingService, T> action = (service, args) => callback.Invoke(args);
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Subscribe<T1, T2>(string message, Action<T1, T2> callback)
        {
            Action<MessagingService, Tuple<T1, T2>> action = (service, args) => callback.Invoke(args.Item1, args.Item2);
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Subscribe<T1, T2, T3>(string message, Action<T1, T2, T3> callback)
        {
            Action<MessagingService, Tuple<T1, T2, T3>> action = (service, args) => callback.Invoke(args.Item1, args.Item2, args.Item3);
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Subscribe<T1, T2, T3, T4>(string message, Action<T1, T2, T3, T4> callback)
        {
            Action<MessagingService, Tuple<T1, T2, T3, T4>> action = (service, args) => callback.Invoke(args.Item1, args.Item2, args.Item3, args.Item4);
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Subscribe<T1, T2, T3, T4, T5>(string message, Action<T1, T2, T3, T4, T5> callback)
        {
            Action<MessagingService, Tuple<T1, T2, T3, T4, T5>> action = (service, args) => callback.Invoke(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5);
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void Subscribe<T1, T2, T3, T4, T5, T6>(string message, Action<T1, T2, T3, T4, T5, T6> callback)
        {
            Action<MessagingService, Tuple<T1, T2, T3, T4, T5, T6>> action = (service, args) => callback.Invoke(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5, args.Item6);
            MessagingCenter.Subscribe(this, message, action);
        }

        public virtual void SubscribeAsync(string message, Func<Task> callback)
        {
            Action action = async () => await callback.Invoke();
            Current.Subscribe(message, action);
        }

        public virtual void SubscribeAsync<T>(string message, Func<T, Task> callback)
        {
            Action<T> action = async args => await callback.Invoke(args);
            Current.Subscribe(message, action);
        }

        public virtual void SubscribeAsync<T1, T2>(string message, Func<T1, T2, Task> callback)
        {
            Action<T1, T2> action = async (arg1, arg2) => await callback.Invoke(arg1, arg2);
            Current.Subscribe(message, action);
        }

        public virtual void SubscribeAsync<T1, T2, T3>(string message, Func<T1, T2, T3, Task> callback)
        {
            Action<T1, T2, T3> action = async (arg1, arg2, arg3) => await callback.Invoke(arg1, arg2, arg3);
            Current.Subscribe(message, action);
        }

        public virtual void SubscribeAsync<T1, T2, T3, T4>(string message, Func<T1, T2, T3, T4, Task> callback)
        {
            Action<T1, T2, T3, T4> action = async (arg1, arg2, arg3, arg4) => await callback.Invoke(arg1, arg2, arg3, arg4);
            Current.Subscribe(message, action);
        }

        public virtual void SubscribeAsync<T1, T2, T3, T4, T5>(string message, Func<T1, T2, T3, T4, T5, Task> callback)
        {
            Action<T1, T2, T3, T4, T5> action = async (arg1, arg2, arg3, arg4, arg5) => await callback.Invoke(arg1, arg2, arg3, arg4, arg5);
            Current.Subscribe(message, action);
        }

        public virtual void SubscribeAsync<T1, T2, T3, T4, T5, T6>(string message, Func<T1, T2, T3, T4, T5, T6, Task> callback)
        {
            Action<T1, T2, T3, T4, T5, T6> action = async (arg1, arg2, arg3, arg4, arg5, arg6) => await callback.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
            Current.Subscribe(message, action);
        }

        public virtual void Unsubscribe(string message)
        {
            MessagingCenter.Unsubscribe<MessagingService>(this, message);
        }

        public virtual void Unsubscribe<T>(string message)
        {
            MessagingCenter.Unsubscribe<MessagingService, T>(this, message);
        }

        public virtual void Unsubscribe<T1, T2>(string message)
        {
            MessagingCenter.Unsubscribe<MessagingService, Tuple<T1, T2>>(this, message);
        }

        public virtual void Unsubscribe<T1, T2, T3>(string message)
        {
            MessagingCenter.Unsubscribe<MessagingService, Tuple<T1, T2, T3>>(this, message);
        }

        public virtual void Unsubscribe<T1, T2, T3, T4>(string message)
        {
            MessagingCenter.Unsubscribe<MessagingService, Tuple<T1, T2, T3, T4>>(this, message);
        }

        public virtual void Unsubscribe<T1, T2, T3, T4, T5>(string message)
        {
            MessagingCenter.Unsubscribe<MessagingService, Tuple<T1, T2, T3, T4, T5>>(this, message);
        }

        public virtual void Unsubscribe<T1, T2, T3, T4, T5, T6>(string message)
        {
            MessagingCenter.Unsubscribe<MessagingService, Tuple<T1, T2, T3, T4, T5, T6>>(this, message);
        }
    }
}
