using Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence.Entities;

namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence.Conversion
{
    public class ProductConverter
    {
        public static Product ConversionToPersistence(Core.Domain.Model.Product source)
        {
            return new Product(source.Name, source.Number, source.Price);
        }

        public static Core.Domain.Model.Product ConversionToCoreModel(Product source)
        {
            return new Core.Domain.Model.Product(source.Name, source.Number, source.Price);
        }
    }
}
