namespace Accso.Ecommerce.Onion.Sales.Core.Domain.Model
{
    /// <summary>
    /// Defines the structure of a product and its business logic
    /// </summary>
    public class Product
    {

        private static readonly String ExpectedNumberStartString = "IT"; 

        public Product(String name, String number, Decimal price)
        {
            ThrowExceptionWhenNumberDoesNotStartWithIT(number);
            ThrowExceptionWhenPriceIsLessOneEuro(price);
            this.Name = name;
            this.Price = price;
            this.Number = number;
        }
        public String Name { get; private set; }
        public String Number { get; private set; }
        public Decimal Price { get; private set; }

        private static void ThrowExceptionWhenNumberDoesNotStartWithIT(String number)
        {
           if (!number.StartsWith("IT"))
            {
                throw new ArgumentException(String.Concat("The number ", number, " must start with ", ExpectedNumberStartString));
            }
        }

        private static void ThrowExceptionWhenPriceIsLessOneEuro(Decimal price)
        {
            if (Decimal.Compare(price, 1) < 0)
            {
                throw new ArgumentException(String.Concat("The price ", price, " must not be little than 1 Euro"));
            }
        }
    }
}
