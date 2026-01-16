using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                var Products = _Context.Products.AsNoTracking().ToList();
                if (Products is null)
                {
                    return NotFound();
                }
                return Products;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> GetID(int id)
        {
            try
            {
                var Product = _Context.Products.AsNoTracking().FirstOrDefault(p => p.ProductID == id);
                if (Product is null)
                {
                    return NotFound();
                }
                return Product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                _Context.Products.Add(product);
                _Context.SaveChanges();

                return new CreatedAtRouteResult("GetProduct", new { id = product.ProductID }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            try
            {
                if (id != product.ProductID)
                {
                    return BadRequest();
                }

                _Context.Entry(product).State = EntityState.Modified;
                _Context.SaveChanges();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
    }
}
