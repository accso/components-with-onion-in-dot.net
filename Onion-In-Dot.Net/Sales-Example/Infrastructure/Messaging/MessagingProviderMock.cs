using Accso.Ecommerce.Onion.Sales.Core.Application;
using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Model;
using Accso.Ecommerce.Onion.Sales.Infrastructure.Messaging.Model;

namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Messaging
{
    public class MessagingProviderMock : ISalesMessaging
    {


        public void SendNewCatalogCreated(Catalog createdCatalog)
        {
            var newCatalogCreatedEvent = new CatalogCreatedEvent(createdCatalog.Name, createdCatalog.Description);
            PublishEvent(newCatalogCreatedEvent);
        }

        public void SendNewProductCreated(string catalogName, Product createdProduct)
        {
            var newProductCreatedEvent = new ProductCreatedEvent(createdProduct.Name, createdProduct.Number, createdProduct.Price,
                catalogName);
            PublishEvent(newProductCreatedEvent);
        }

        private static void PublishEvent(Event eventToPublish)
        {
            Console.WriteLine(eventToPublish);
        }
    }
}
