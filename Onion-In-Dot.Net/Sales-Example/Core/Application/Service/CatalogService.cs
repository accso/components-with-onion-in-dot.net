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
            catalogRepository.CreateCatalog(coreCatalog);
            var createdCatalog = CatalogConverter.ConversionToService(coreCatalog);
            messaging.SendNewCatalogCreated(createdCatalog);
            return catalog;
        }

        public Catalog FindCatalogByName(String name)
        {
            var foundCatalog = FindCoreCatalogByName(name);

            return CatalogConverter.ConversionToService(foundCatalog);
        }

        private Accso.Ecommerce.Onion.Sales.Core.Domain.Model.Catalog FindCoreCatalogByName(String name)
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
            if (catalogToAddProduct.GetProduct(newProduct.Number) != null)
            {
                throw new ProductAlreadyExistsException(newProduct.Name);
            }
            var convertedNdewProduct = ProductConverter.ConversionToCoreModel(newProduct);
            catalogToAddProduct.AddProductToCatalog(convertedNdewProduct);
            this.catalogRepository.UpdateCatalog(catalogToAddProduct);
            this.messaging.SendNewProductCreated(catalogName, newProduct);
            return CatalogConverter.ConversionToService(catalogToAddProduct);
        }

        public Product FindProductByName(string catalogName, string productNumber)
        {
            var catalogWithRequestedProduct = FindCoreCatalogByName(catalogName);
            var foundProduct = catalogWithRequestedProduct.GetProduct(productNumber);
            if (foundProduct == null)
            {
                throw new ProductNotFoundException(productNumber);
            } 
            return ProductConverter.ConversionToService(foundProduct);
        }
    }
}
