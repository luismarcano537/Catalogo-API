using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _repository.Get();

            if (products is null)
            {
                return NotFound("Products is empty");
            }

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductID")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Product> GetID(int id)
        {
            var Product = _repository.GetByID(P => P.ProductID == id);
            if (Product is null)
            {
                return NotFound($"The requested product with ID:{id} was not found.");
            }
            return Ok(Product);
        }

        [HttpGet("category/{id}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Product>> GetIncludeCategory(int id)
        {
            var productByCategory = _repository.GetProductsByCategory(id);

            if (productByCategory is null)
            {
                return NotFound("The Category is empty");
            }

            return Ok(productByCategory);
        }

        [HttpGet("supplier/{id}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Product>> GetIncludeSupplier(int id)
        {
            var productBySupplier = _repository.GetProductsBySupplier(id);

            if (productBySupplier is null)
            {
                return NotFound("The Supplier is empty");
            }

            return Ok(productBySupplier);
        }


        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            _repository.Create(product);

            return new CreatedAtRouteResult("GetProduct", new { id = product.ProductID }, product);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest("Unable to update product with ID: {id}");
            }

            var productUpdate = _repository.Update(product);

            return Ok(productUpdate);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            var product = _repository.GetByID(P => P.ProductID == id);

            if (product is null)
            {
                return NotFound($"The requested product with ID:{id} was not found.");
            }

            var productDeleted = _repository.Delete(product);

            return Ok(productDeleted);
        }
    }
}
