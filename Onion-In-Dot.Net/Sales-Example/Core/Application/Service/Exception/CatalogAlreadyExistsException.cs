namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service.Exception
{
    public class CatalogAlreadyExistsException : ApplicationException
    {
        public CatalogAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}
