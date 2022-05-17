using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Conversion;
using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Exception;
using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Model;

namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service
{
    public class CatalogService : ICatalogService
    {

        private readonly ICatalogRepository catalogRepository;
        private readonly ISalesMessaging messaging;

        public CatalogService(ICatalogRepository catalogRepository, ISalesMessaging messaging)
        {
            this.catalogRepository = catalogRepository;
            this.messaging = messaging;
        }

        public Catalog CreateNewCatalog(Catalog catalog)
        {
            if (catalogRepository.FindCatalogByName(catalog.Name) != null)
            {
                throw new CatalogAlreadyExistsException(catalog.Name);
            }

            var coreCatalog = CatalogConverter.ConversionToCoreModel(catalog);
            catalogRepository.CreateCatalog(CatalogConverter.ConversionToService(coreCatalog));
            var createdCatalog = CatalogConverter.ConversionToService(coreCatalog);
            messaging.SendNewCatalogCreated(createdCatalog);
            return catalog;
        }

        public Catalog FindCatalogByName(String name)
        {
            var foundCatalog = FindCoreCatalogByName(name);

            return foundCatalog;
        }

        private Catalog FindCoreCatalogByName(String name)
        {
            var foundCatalog = this.catalogRepository.FindCatalogByName(name);

            if (foundCatalog == null)
            {
                throw new CatalogNotFoundException(name);
            }
            return foundCatalog;
        }

        public Catalog AddProduct(string catalogName, Product newProduct)
        {
            var catalogToAddProduct = FindCoreCatalogByName(catalogName);
            var coreCatalog = CatalogConverter.ConversionToCoreModel(catalogToAddProduct);
            if (coreCatalog.GetProduct(newProduct.Number) != null)
            {
                throw new ProductAlreadyExistsException(newProduct.Name);
            }
            var convertedNewProduct = ProductConverter.ConversionToCoreModel(newProduct);
            coreCatalog.AddProductToCatalog(convertedNewProduct);
            this.catalogRepository.UpdateCatalog(catalogToAddProduct);
            this.messaging.SendNewProductCreated(catalogName, newProduct);
            return CatalogConverter.ConversionToService(coreCatalog);
        }

        public Product FindProductByName(string catalogName, string productNumber)
        {
            var catalogWithRequestedProduct = FindCoreCatalogByName(catalogName);
            var coreCatalog = CatalogConverter.ConversionToCoreModel(catalogWithRequestedProduct);
            var foundProduct = coreCatalog.GetProduct(productNumber);
            if (foundProduct == null)
            {
                throw new ProductNotFoundException(productNumber);
            } 
            return ProductConverter.ConversionToService(foundProduct);
        }
    }
}
