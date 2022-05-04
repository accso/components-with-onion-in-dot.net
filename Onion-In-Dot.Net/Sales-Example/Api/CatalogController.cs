using Accso.Ecommerce.Onion.Sales.Api.Conversion;
using Accso.Ecommerce.Onion.Sales.Api.Model;
using Accso.Ecommerce.Onion.Sales.Core.Application.Service;
using Accso.Ecommerce.Onion.Sales.Core.Application.Service.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Accso.Ecommerce.Onion.Sales.Api
{
    [ApiController]
    [Route("/catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        /// <summary>
        /// creates a new catalog
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateNewCatalog(Catalog catalog)
        {
            var applicationCatalog = CatalogConverter.ConversionToService(catalog);
            Core.Application.Service.Model.Catalog createdCatalog;
            try
            {
                createdCatalog = catalogService.CreateNewCatalog(applicationCatalog);
            } catch(CatalogAlreadyExistsException)
            {
                return BadRequest(string.Concat("The catalog ", catalog.Name, " already exists"));
            }
            
            var result = CatalogConverter.ConversionToAPI(createdCatalog);
            return Created("", result);
        }

        /// <summary>
        /// gets an existing catalog
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public IActionResult GetCatalogByName(String name)
        {
            Core.Application.Service.Model.Catalog foundCatalog;
            try
            {
                foundCatalog = catalogService.FindCatalogByName(name);
            }
            catch (CatalogNotFoundException)
            {                
               return NotFound(name);
            }
            
           
            var result = CatalogConverter.ConversionToAPI(foundCatalog);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new product and adds it to a catalog
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        [HttpPost("{catalogName}/product")]
        public IActionResult AddProduct(String catalogName, Product newProduct)
        {
            var requestToAdd = ProductConverter.ConversionToService(newProduct);
            Core.Application.Service.Model.Catalog catalogWithAddedProduct;
            try
            {
                catalogWithAddedProduct = catalogService.AddProduct(catalogName, requestToAdd);
            }
            catch (CatalogNotFoundException)
            {

                return NotFound(catalogName);
            }
            catch (ProductAlreadyExistsException)
            {
                return BadRequest(string.Concat("A product with the number ", newProduct.Number, " already exists"));
            }

            var result = CatalogConverter.ConversionToAPI(catalogWithAddedProduct);
            return Created("", result);
        }

        /// <summary>
        /// gets an existing product which belongs to a specific catalog 
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="productNumber"></param>
        /// <returns></returns>
        [HttpGet("{catalogName}/product/{productNumber}")]
        public IActionResult FindProduct(String catalogName, String productNumber)
        {
            Core.Application.Service.Model.Product foundProduct;
            try
            {
                foundProduct = catalogService.FindProductByName(catalogName, productNumber);
            }            
            catch (CatalogNotFoundException)
            {

                return NotFound(catalogName);
            }
            catch (ProductNotFoundException)
            {
                return NotFound(productNumber);
            }

            var result = ProductConverter.ConversionToAPI(foundProduct);
            return Ok(result);
        }
    }
}
