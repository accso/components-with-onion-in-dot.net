using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Model;

namespace Accso.Ecommerce.Onion.Sales.Core.Application
{
    /// <summary>
    /// Defines the interface which is needed to sends messages to other systems
    /// </summary>
    public interface ISalesMessaging
    {
        /// <summary>
        /// notifies that a new catalaog was creadted
        /// </summary>
        /// <param name="createdCatalog"></param>
        /// <returns></returns>
        public void SendNewCatalogCreated(Catalog createdCatalog);

        /// <summary>
        /// notifies that a new product was created
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="createdProduct"></param>
        /// <returns></returns>
        public void SendNewProductCreated(String catalogName, Product createdProduct);
    }
}
