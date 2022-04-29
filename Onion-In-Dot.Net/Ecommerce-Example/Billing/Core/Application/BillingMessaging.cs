using Accso.Ecommerce.Common;

namespace Accso.Ecommerce.Billing.Core.Application
{
    public interface BillingMessaging
    {
        public void Send(Event billingEvent);
    }
}
