namespace Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence.Entities
{
    public class Catalog
    {

        private IList<Product> products;

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
