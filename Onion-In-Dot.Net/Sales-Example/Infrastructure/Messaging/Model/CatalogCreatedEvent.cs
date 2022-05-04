namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Messaging.Model
{
    public class CatalogCreatedEvent : Event
    {
        public CatalogCreatedEvent(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public String Name { get; private set; }
        public String Description { get; private set; }

        public override string ToString()
        {
            return string.Concat(nameof(Name), " : ", Name, " - ", nameof(Description), " : ", Description);
        }
    }
}
