using Accso.Ecommerce.Sales.API;
using Accso.Ecommerce.Shipping.Core.Application;

namespace Accso.Ecommerce.Sales.Core.Application
{
    public class SalesEventProducer
    {
        private readonly List<Type> allEventTypes = new()
        {
            typeof(CartUpdatedEvent),
            typeof(CatalogChangedEvent),
            typeof(OrderPlacedEvent),
            typeof(SpecialSalesPhaseEndedEvent),
            typeof(SpecialSalesPhaseStartedEvent)
        };

        private ShippingMessaging messageBus;   // this is wrong, tests will fail
                                                // fix by changing type to: SalesMessaging messageBus
                                                //TODO: Shall this also be part of the .Net

        private void ProduceCartUpdatedEvent()
        {
            messageBus.Send(new CartUpdatedEvent());
        }

        private void ProduceCatalogChangedEvent()
        {
            messageBus.Send(new CatalogChangedEvent());
        }

        private void ProduceOrderPlacedEvent()
        {
            messageBus.Send(new OrderPlacedEvent());
        }

        private void ProduceSpecialSalesPhaseEndedEvent()
        {
            messageBus.Send(new SpecialSalesPhaseEndedEvent());
        }

        private void ProduceSpecialSalesPhaseStartedEvent()
        {
            messageBus.Send(new SpecialSalesPhaseStartedEvent());
        }
    }
}
