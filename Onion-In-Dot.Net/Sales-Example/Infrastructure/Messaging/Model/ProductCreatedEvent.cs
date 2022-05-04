namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Messaging.Model
{
    public class ProductCreatedEvent : Event
    {
        public ProductCreatedEvent(string name, string number, decimal price, string catalogName)
        {
            Name = name;
            Number = number;
            Price = price;
            CatalogName = catalogName;
        }

        public String CatalogName { get; set; }
        public String Name { get; set; }
        public String Number { get; set; }
        public Decimal Price { get; set; }

        public override string ToString()
        {
           return string.Concat(nameof(CatalogName), " : ", CatalogName, " - ", nameof(Name), " : ", Name,
                " - ", nameof(Number), " - ", nameof(Price), " : ", Price);
        }
    }
}
