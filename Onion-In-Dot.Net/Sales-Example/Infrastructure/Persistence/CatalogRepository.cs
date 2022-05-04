using Accso.Ecommerce.Onion.Sales.Core.Application;
using Accso.Ecommerce.Onion.Sales.Core.Domain.Model;
using Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence.Conversion;

namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly StorageMock storageMock;

        public CatalogRepository(StorageMock storageMock)
        {
            this.storageMock = storageMock;
        }

        public void CreateCatalog(Catalog catalog)
        {
            this.storageMock.AddNewCatalog(CatalogConverter.ConversionToPersistence(catalog));
        }

        public void DeleteCatalog(Catalog catalog)
        {
            this.storageMock.RemoveCatalog(CatalogConverter.ConversionToPersistence(catalog));
        }

        public Catalog? FindCatalogByName(string catalogName)
        {
            var foundCatalog = this.storageMock.FindCatalogByName(catalogName);
            return (foundCatalog != null) ? CatalogConverter.ConversionToCoreModel(foundCatalog) : null;
        }

        public void UpdateCatalog(Catalog catalog)
        {
            this.storageMock.UpdateCatalog(CatalogConverter.ConversionToPersistence(catalog));
        }
    }
}
