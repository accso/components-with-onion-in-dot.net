using Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence.Entities;

namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence
{
    public class StorageMock
    {
        private static readonly Dictionary<Catalog, IList<Product>> CatalogProductStorage = new ();

        public void AddNewCatalog(Catalog catalog)
        {
            CatalogProductStorage.Add(catalog, new List<Product>());
        }

        public void RemoveCatalog(Catalog catalog)
        {
            CatalogProductStorage.Remove(catalog);
        }

        public void UpdateCatalog(Catalog catalog)
        {
            if (CatalogProductStorage.Count > 0)
            {
                var foundCatalog = FindCatalogByName(catalog.Name);
                if (foundCatalog == null)
                {
                    throw new ArgumentException(String.Concat("The catalog with name ", catalog.Name, " does not exist"));
                }
                CatalogProductStorage.Remove(foundCatalog);
                CatalogProductStorage.Add(catalog, catalog.Products);               
            }
            else
            {
                CatalogProductStorage.Add(catalog, catalog.Products);
            }
        }

        public Catalog? FindCatalogByName(string name)
        {
            return CatalogProductStorage.Where(catalog => catalog.Key.Name.Equals(name))
               .Select(catalog => catalog.Key).FirstOrDefault();
        }
    }
}
