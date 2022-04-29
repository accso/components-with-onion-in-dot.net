using Accso.Ecommerce.Common;

namespace Accso.Ecommerce.Shipping.Core.Application
{
    public interface ShippingMessaging
    {
        public void Send(Event shippingEvent);
    }
}
