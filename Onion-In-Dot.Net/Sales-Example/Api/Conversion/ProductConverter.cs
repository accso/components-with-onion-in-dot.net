using Accso.Ecommerce.Onion.Sales.Api.Model;

namespace Accso.Ecommerce.Onion.Sales.Api.Conversion
{
    public class ProductConverter
    {
        public static Product ConversionToAPI(Core.Application.Service.Model.Product source)
        {
            return new Product(source.Name, source.Number, source.Price);
        }

        public static Core.Application.Service.Model.Product ConversionToService(Product source)
        {
            return new Core.Application.Service.Model.Product(source.Name, source.Number, source.Price);
        }
    }
}
