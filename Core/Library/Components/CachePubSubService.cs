using System;
using Atlas.Forms.Interfaces.Components;
using Atlas.Forms.Interfaces.Services;
using Atlas.Forms.Interfaces.Utilities;
using Atlas.Forms.Services;
using Atlas.Forms.Utilities;
using Xamarin.Forms;

namespace Atlas.Forms.Components
{
    public class CachePubSubService : ICachePubSubService
    {
        public static ICachePublisher Publisher => Instance.Current;

        public static ICacheSubscriber Subscriber => Instance.Current;

        protected static ILazySingleton<ICachePubSubService> Instance { get; set; }
            = new LazySingleton<ICachePubSubService>();

        public static void SetCurrent(Func<ICachePubSubService> func)
        {
            Instance.SetCurrent(func);
        }

        protected string id => "__CacheEventMessager";

        protected string OnPageAppeared => "OnPageAppeared" + id;

        protected string OnPageDisappeared => "OnPageDisappeared" + id;

        protected string OnPageCreated => "OnPageCreated" + id;

        public virtual void SendPageAppearedMessage(Page page)
        {
            MessagingService.Current.SendMessage(OnPageAppeared, page);
        }

        public virtual void SendPageDisappearedMessage(Page page)
        {
            MessagingService.Current.SendMessage(OnPageDisappeared, page);
        }

        public virtual void SendPageCreatedMessage(Page page)
        {
            MessagingService.Current.SendMessage(OnPageCreated, page);
        }

        public virtual void SubscribePageAppeared(Action<Page> action)
        {
            MessagingService.Current.Subscribe(OnPageAppeared, action);
        }

        public virtual void SubscribePageDisappeared(Action<Page> action)
        {
            MessagingService.Current.Subscribe(OnPageDisappeared, action);
        }

        public virtual void SubscribePageCreated(Action<Page> action)
        {
            MessagingService.Current.Subscribe(OnPageCreated, action);
        }

        public virtual void UnsubscribePageAppeared()
        {
            MessagingService.Current.Unsubscribe<Page>(OnPageAppeared);
        }

        public virtual void UnsubscribePageDisappeared()
        {
            MessagingService.Current.Unsubscribe<Page>(OnPageDisappeared);
        }

        public virtual void UnsubscribePageCreated()
        {
            MessagingService.Current.Unsubscribe<Page>(OnPageCreated);
        }
    }
}
