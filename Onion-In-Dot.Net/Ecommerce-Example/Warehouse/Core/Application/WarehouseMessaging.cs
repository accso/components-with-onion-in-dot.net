using Accso.Ecommerce.Common;

namespace Accso.Ecommerce.Warehouse.Core.Application
{
    internal interface WarehouseMessaging
    {
        public void Send(Event warehouseEvent);
    }
}
