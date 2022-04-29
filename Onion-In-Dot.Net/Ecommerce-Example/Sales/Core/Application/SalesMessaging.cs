using Accso.Ecommerce.Common;

namespace Accso.Ecommerce.Sales.Core.Application
{
    public interface SalesMessaging
    {
        public void Send(Event salesEvent);
    }
}
