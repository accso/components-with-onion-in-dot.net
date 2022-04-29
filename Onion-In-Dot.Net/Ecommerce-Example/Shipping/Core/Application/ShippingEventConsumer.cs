using Accso.Ecommerce.Sales.API;

namespace Accso.Ecommerce.Shipping.Core.Application
{
    public class ShippingEventConsumer
    {
        //TODO: Is it correct that the shipping consumer knows the structure of sales
        private void ConsumeOrderPlacedEvent(OrderPlacedEvent orderPlacedEvent)
        {
            // nope
        }
    }
        
}
