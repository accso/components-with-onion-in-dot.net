namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service.Exception
{
    public class ProductAlreadyExistsException : ApplicationException
    {
        public ProductAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}
