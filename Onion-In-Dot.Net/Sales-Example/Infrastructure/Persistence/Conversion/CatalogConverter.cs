using Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence.Entities;

namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence.Conversion
{
    public class CatalogConverter
    {
        public static Catalog ConversionToPersistence(Core.Domain.Model.Catalog source)
        {
            var destination = new Catalog(source.Name, source.Description)
            {
                Products = (source.Products != null) ?
                source.Products.Select(product => ProductConverter.ConversionToPersistence(product)).ToList() 
                : new List<Product>()
            };
            return destination;
        }

        public static Core.Domain.Model.Catalog ConversionToCoreModel(Catalog source)
        {
            IList<Core.Domain.Model.Product> products = (source.Products != null) ?
                source.Products.Select(product => ProductConverter.ConversionToCoreModel(product)).ToList() 
                : new List<Core.Domain.Model.Product>();
            var destination = new Core.Domain.Model.Catalog(source.Name, source.Description, products);
            return destination;
        }
    }
}
