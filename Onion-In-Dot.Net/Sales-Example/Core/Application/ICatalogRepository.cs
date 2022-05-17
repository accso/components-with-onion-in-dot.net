
using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Model;

namespace Accso.Ecommerce.Onion.Sales.Core.Application
{
    /// <summary>
    /// Defines the interface of accessing persisted catalog data
    /// </summary>
    public interface ICatalogRepository
    {
        /// <summary>
        /// searchs for a specific catalog
        /// </summary>
        /// <param name="catalogName">name of the requested catalog</param>
        /// <returns>the found catalog or null</returns>
        public Catalog? FindCatalogByName(String catalogName);

        /// <summary>
        /// updates a catalog
        /// </summary>
        /// <param name="catalog">catalog which should be updated</param>
        public void UpdateCatalog(Catalog catalog);

        /// <summary>
        /// deletes a catalog
        /// </summary>
        /// <param name="catalog">catalog whoch should be deleted</param>
        public void DeleteCatalog(Catalog catalog);

        /// <summary>
        /// crates a new catalog
        /// </summary>
        /// <param name="catalog"></param>
        public void CreateCatalog(Catalog catalog);
    }
}
