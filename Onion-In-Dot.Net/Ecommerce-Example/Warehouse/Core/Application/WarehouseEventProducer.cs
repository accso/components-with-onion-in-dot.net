using Accso.Ecommerce.Warehouse.Api;

namespace Accso.Ecommerce.Warehouse.Core.Application
{
    public class WarehouseEventProducer
    {
        private readonly List<Type> allEventTypes = new()
        {
            typeof(NewGoodsReceivedEvent),
            typeof(ProductRunsOutOfStockEvent)
        };

        private WarehouseMessaging messageBus;

        private void ProduceNewGoodsReceivedEvent()
        {
            messageBus.Send(new NewGoodsReceivedEvent());
        }

        private void ProduceProductRunsOutOfStockEvent()
        {
            messageBus.Send(new ProductRunsOutOfStockEvent());
        }
    }
}
