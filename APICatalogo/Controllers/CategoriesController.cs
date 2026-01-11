using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly APICatalogoContext _Context;

        public CategoriesController(APICatalogoContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _Context.Categories.ToList();

            if (categories is null)
            {
                return NotFound("The Categories is empty");
            }

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult GetCategory(int id)
        {
            var category = _Context.Categories.FirstOrDefault(C => C.CategoryID == id);

            if (category is null)
            {
                return BadRequest("Unable to find the requested category");
            }

            return Ok(category);
        }
    }
}
