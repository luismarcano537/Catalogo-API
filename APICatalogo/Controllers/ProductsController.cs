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

        [HttpGet("{id:int}")]
        public ActionResult<Product> GetID(int id)
        {
            var Product = _Context.Products.FirstOrDefault(p => p.ProductID == id);
            if (Product is null)
            {
                return NotFound();
            }
            return Product;
        }
    }
}
