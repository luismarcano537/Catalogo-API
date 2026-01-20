using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly APICatalogoContext _Context;

        public ProductsController(APICatalogoContext context)
        {
            _Context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var Products = _Context.Products.AsNoTracking().ToList();
            if (Products is null)
            {
                return NotFound();
            }
            return Products;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Product> GetID(int id)
        {
            var Product = _Context.Products.AsNoTracking().FirstOrDefault(p => p.ProductID == id);
            if (Product is null)
            {
                return NotFound();
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
            _Context.Products.Add(product);
            _Context.SaveChanges();

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

            _Context.Entry(product).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            var product = _Context.Products.FirstOrDefault(P => P.ProductID == id);

            if (product is null)
            {
                return NotFound("Product not found in the database.");
            }

            _Context.Products.Remove(product);
            _Context.SaveChanges();

            return Ok(product);
        }
    }
}
