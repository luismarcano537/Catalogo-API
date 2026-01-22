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

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Product> GetID(int id)
        {
            var Product = _repository.GetByID(id);
            if (Product is null)
            {
                return NotFound($"The requested product with ID:{id} was not found.");
            }
            return Product;
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            _repository.Update(product);

            return new CreatedAtRouteResult("GetProduct", new { id = product.ProductID }, product);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            bool productUpdate = _repository.Update(product);

            if (productUpdate)
            {
                return Ok(product);
            }
            else
            {
                return StatusCode(500, $"Unable to update product with ID: {id}");
            }

        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            bool deleteProduct = _repository.Delete(id);

            if (!deleteProduct)
            {
                return Ok($"The product with ID:{id} has been successfully removed.");
            }
            else
            {
                return StatusCode(500, $"Unable to removed the product with ID: {id}");
            }
        }
    }
}
