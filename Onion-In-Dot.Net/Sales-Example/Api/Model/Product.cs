namespace Accso.Ecommerce.Onion.Sales.Api.Model
{
    [Serializable]
    public class Product
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public Product(String name, String number, Decimal price)
        {
            this.Number = number;
            this.Name = name;
            this.Price = price;
        }

        public String Name { get; set; }
        public String Number { get; set; }
        public Decimal Price { get; set; }
    }
}
