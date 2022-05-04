namespace Accso.Ecommerce.Onion.Sales.Core.Application.Service.Exception
{
    public class CatalogNotFoundException : ApplicationException
    {
        public CatalogNotFoundException(string? message) : base(message)
        {
        }
    }
}
