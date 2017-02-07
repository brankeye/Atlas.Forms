using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Forms.Interfaces.Services;
using Xamarin.Forms;

namespace Atlas.Forms.Interfaces.Components
{
    public interface ICachePubSubService : ICachePublisher, ICacheSubscriber
    {
        
    }

    public interface ICachePublisher
    {
        void SendPageAppearedMessage(Page page);

        void SendPageDisappearedMessage(Page page);

        void SendPageCreatedMessage(Page page);
    }

    public interface ICacheSubscriber
    {
        void SubscribePageAppeared(Action<Page> action);

        void SubscribePageDisappeared(Action<Page> action);

        void SubscribePageCreated(Action<Page> action);

        void UnsubscribePageAppeared();

        void UnsubscribePageDisappeared();

        void UnsubscribePageCreated();
    }
}
