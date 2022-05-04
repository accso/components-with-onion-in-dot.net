using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Model;

namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service
{
    public interface ICatalogService
    {
        /// <summary>
        /// creates a new cataolog
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        public Catalog CreateNewCatalog(Catalog catalog);

        /// <summary>
        /// finds a catalog
        /// </summary>
        /// <param name="catalogName"></param>
        /// <returns></returns>
        public Catalog FindCatalogByName(String catalogName);

        /// <summary>
        /// adds a product to a catalog
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        public Catalog AddProduct (String catalogName, Product newProduct);

        /// <summary>
        /// finds a product
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public Product FindProductByName(String catalogName, String productName);
    }
}
