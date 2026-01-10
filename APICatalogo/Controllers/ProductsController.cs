using APICatalogo.Context;
using APICatalogo.Models;
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
            var Products = _Context.Products.ToList();
            if (Products is null)
            {
                return NotFound();
            }
            return Products;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> GetID(int id)
        {
            var Product = _Context.Products.FirstOrDefault(p => p.ProductID == id);
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
    }
}
