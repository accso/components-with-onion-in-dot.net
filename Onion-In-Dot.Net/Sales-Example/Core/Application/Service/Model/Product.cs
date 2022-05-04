namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service.Model
{
    public class Product
    {
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
