using Accso.Ecommerce.Shipping.API;

namespace Accso.Ecommerce.Shipping.Core.Application
{
    public class ShippingEventProducer
    {
        private readonly List<Type> allEventTypes = new()
        {
            typeof(DeliveryDeliveredEvent),
            typeof(DeliveryPreparedEvent),
            typeof(DeliveryRetourEvent),
            typeof(DeliverySentOutEvent)
        };

        private ShippingMessaging messageBus;


        private void ProduceDeliveryDeliveredEvent()
        {
            messageBus.Send(new DeliveryDeliveredEvent());
        }

        private void ProduceDeliveryPreparedEvent()
        {
            messageBus.Send(new DeliveryPreparedEvent());
        }

        private void ProduceDeliveryRetourEvent()
        {
            messageBus.Send(new DeliveryRetourEvent());
        }

        private void ProduceDeliverySentOutEvent()
        {
            messageBus.Send(new DeliverySentOutEvent());
        }

    }
}
