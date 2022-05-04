using Accso.Ecommerce.Onion.Sales.Api.Model;

namespace Accso.Ecommerce.Onion.Sales.Api.Conversion
{
    public class CatalogConverter
    {

        public static Catalog ConversionToAPI(Core.Application.Service.Model.Catalog source)
        {
            var destination = new Catalog(source.Name, source.Description)
            {
                Products = (source.Products != null) ?
                source.Products.Select(product => ProductConverter.ConversionToAPI(product)).ToList()
                : new List<Product>()
            };
            return destination;
        }

        public static Core.Application.Service.Model.Catalog ConversionToService(Catalog source)
        {
            IList<Core.Application.Service.Model.Product> products = (source.Products != null) ?
                source.Products.Select(product => ProductConverter.ConversionToService(product)).ToList()
                : new List<Core.Application.Service.Model.Product>();
            var destination = new Core.Application.Service.Model.Catalog(source.Name, source.Description, products);
            return destination;
        }
    }
}
