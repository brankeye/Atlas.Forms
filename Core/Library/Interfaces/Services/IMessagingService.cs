using System;
using System.Threading.Tasks;

// Xamarin.Forms.Toolkit
// Source: https://github.com/jamesmontemagno/xamarin.forms-toolkit/blob/master/FormsToolkit/FormsToolkit/Interfaces/IMessagingService.cs

namespace Atlas.Forms.Interfaces.Services
{
    public interface IMessagingService
    {
        /// <summary>
        /// Subscribe the specified message and callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="callback">Callback.</param>
        void Subscribe(string message, Action callback);

        /// <summary>
        /// Subscribe the specified message and callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="callback">Callback.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void Subscribe<TArgs>(string message, Action<TArgs> callback);

        /// <summary>
        /// Subscribe the specified message and callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="callback">Callback.</param>
        /// /// <param name="useAwait">Choose whether to await the call.</param>
        void SubscribeAsync(string message, Func<Task> callback);

        /// <summary>
        /// Subscribe the specified message and callback.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="callback">Callback.</param>
        /// <param name="useAwait">Choose whether to await the call.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void SubscribeAsync<TArgs>(string message, Func<TArgs, Task> callback);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">Message.</param>
        void SendMessage(string message);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void SendMessage<TArgs>(string message, TArgs args);

        /// <summary>
        /// Unsubscribe the specified message.
        /// </summary>
        /// <param name="message">Message.</param>
        void Unsubscribe(string message);

        /// <summary>
        /// Unsubscribe the specified message and args.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void Unsubscribe<TArgs>(string message);
    }
}
