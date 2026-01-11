using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[Controller]")]
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
            var Products = _Context.Products.AsNoTracking().ToList();
            if (Products is null)
            {
                return NotFound();
            }
            return Products;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
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
