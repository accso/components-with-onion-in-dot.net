namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service.Exception
{
    public class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException(string? message) : base(message)
        {
        }
    }
}
