using Accso.Ecommerce.Billing.Api;

namespace Accso.Ecommerce.Billing.Core.Application
{
    public class BillingEventProducer
    {
        //TODO: What is this list for?
        private readonly List<Type> allEventTypes = new List<Type>()
        {
            typeof(BillCreatedEvent),
            typeof(PaymentDoneEvent)
        };

        private BillingMessaging messageBus;

        private void ProduceBillCreatedEvent()
        {
            messageBus.Send(new BillCreatedEvent());
        }

        private void ProducePaymentDoneEvent()
        {
            messageBus.Send(new PaymentDoneEvent());
        }
    }
}
