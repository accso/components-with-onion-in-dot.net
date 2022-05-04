using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Model;

namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service.Conversion
{
    public class ProductConverter
    {
        public static Product ConversionToService(Core.Domain.Model.Product source)
        {
            return new Product(source.Name, source.Number, source.Price);
        }

        public static Core.Domain.Model.Product ConversionToCoreModel(Product source)
        {
            return new Core.Domain.Model.Product(source.Name, source.Number, source.Price);
        }
    }
}
