namespace Accso.Ecommerce.Onion.Sales.Api.Model
{
    [Serializable]
    public class Catalog
    {
        private IList<Product> products;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Catalog()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public Catalog(String name, String description, IList<Product> products)
        {
            this.Description = description;
            this.Name = name;
            this.products = products;
        }

        public Catalog(String name, String description)
        {
            this.Description = description;
            this.Name = name;
            this.products = new List<Product>();
        }

        public String Name { get; set; }
        public String Description { get; set; }
        public IList<Product> Products
        {
            get
            {
                if (this.products == null)
                {
                    this.products = new List<Product>();
                }
                return this.products;
            }
            set { this.products = value; }
        }
    }
}
