namespace Accso.Ecommerce.Onion.Sales.Core.Domain.Model
{
    /// <summary>
    /// This class defines the structure and logic of a product catalog
    /// </summary>
    public class Catalog
    {
        public Catalog(String name, String description)
        {
            this.Name = name;
            this.Products = new List<Product>();
            this.Description = description;
        }

        public Catalog(String name, String description, IList<Product> products)
        {
            this.Name=name;
            this.Description=description;
            this.Products = products;
        }

        public String Name { get; private set; }
        public String Description { get; private set; }
        public IList<Product> Products { get; private set; }

        /// <summary>
        /// adds a new product to the catalog
        /// </summary>
        /// <param name="newProduct">new peoduct which should be added</param>
        /// <exception cref="ArgumentNullException">if the passed param is null</exception>
        /// <exception cref="ArgumentException">if the catalog already has a product with same number</exception>
        public void AddProductToCatalog(Product newProduct)
        {
            if (newProduct == null)
            {
                throw new ArgumentNullException(nameof(newProduct));
            }

            if (ExistsProductAlready(newProduct))
            {
                throw new ArgumentException(String.Concat("The catalog ", Name, " already has a product with number ", newProduct.Name));
            }

            this.Products.Add(newProduct);
        }

        private bool ExistsProductAlready(Product newProduct)
        {
            return this.Products.Any(p => p.Number.Equals(newProduct.Number));
        }

        /// <summary>
        /// calculates the sum for all product which belomgs to the catalog
        /// </summary>
        /// <returns></returns>
        public Decimal SumPriceOfAllCatalogProducts()
        {
            return this.Products.Sum(p => p.Price);
        }

        public Product? GetProduct(String productNumber)
        {
            Product? foundProduct = null;
            if (Products != null && Products.Count > 0)
            {
                foundProduct = Products.Where(product => product.Number.Equals(productNumber))
                    .FirstOrDefault();
            }
            return foundProduct;
        }

        public void RemoveProduct(String productNumber)
        {
            var foundProduct = GetProduct(productNumber);
            if (foundProduct != null)
            {
                Products.Remove(foundProduct);
            }
        }
    }
}
