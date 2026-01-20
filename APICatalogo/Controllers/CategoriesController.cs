using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Filters;

namespace APICatalogo.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly APICatalogoContext _Context;
        private readonly ILogger _Logger;

        public CategoriesController(APICatalogoContext context, ILogger<CategoriesController> logger)
        {
            _Context = context;
            _Logger = logger;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> Get()
        {
            _Logger.LogInformation("================ GetCategory ====================");
            var categories = _Context.Categories.AsNoTracking().ToList();

            if (categories is null)
            {
                return NotFound("The Categories is empty");
            }

            return Ok(categories);
        }

        [HttpGet("GetInclude")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> GetInclude()
        {
            _Logger.LogInformation("================ GetInclude ====================");
            var CategoryInclude = _Context.Categories.Include(P => P.Products).Where(P => P.CategoryID < 5).AsNoTracking().ToList();

            if (CategoryInclude is null)
            {
                return BadRequest("Unable to display categories and their products");
            }

            return Ok(CategoryInclude);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult GetCategory(int id)
        {
            _Logger.LogInformation("================ GetCategoryID ====================");
            var category = _Context.Categories.AsNoTracking().FirstOrDefault(C => C.CategoryID == id);

            if (category is null)
            {
                return BadRequest("Unable to find the requested category");
            }

            return Ok(category);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Post(Category category)
        {
            _Logger.LogInformation("================ PostCategory ====================");
            if (category is null)
            {

                return BadRequest("Unable to create a new category");
            }

            _Context.Categories.Add(category);
            _Context.SaveChanges();

            return Ok(category);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Put(int id, Category category)
        {
            _Logger.LogInformation("================ PutCategory ====================");
            if (id != category.CategoryID)
            {
                return BadRequest("Please provide a valid ID.");
            }

            _Context.Entry(category).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
                _Logger.LogInformation("================ DeleteCategory ====================");
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
