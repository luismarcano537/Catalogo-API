using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("GetInclude")]
        public ActionResult<IEnumerable<Category>> GetInclude()
        {
            var CategoryInclude = _Context.Categories.Include(P => P.Products).ToList();

            if (CategoryInclude is null)
            {
                return BadRequest("Unable to display categories and their products");
            }

            return Ok(CategoryInclude);
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

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
            {

                return BadRequest("Unable to create a new category");
            }

            _Context.Categories.Add(category);
            _Context.SaveChanges();

            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest("Please provide a valid ID.");
            }

            _Context.Entry(category).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _Context.Categories.FirstOrDefault(C => C.CategoryID == id);

            if (category is null)
            {
                return BadRequest("Unable to find the requested category");
            }

            _Context.Categories.Remove(category);
            _Context.SaveChanges();

            return Ok(category);
        }
    }
}
