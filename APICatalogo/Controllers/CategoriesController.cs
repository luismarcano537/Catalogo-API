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

        public CategoriesController(APICatalogoContext context)
        {
            _Context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                var categories = _Context.Categories.AsNoTracking().ToList();

                if (categories is null)
                {
                    return NotFound("The Categories is empty");
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpGet("GetInclude")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> GetInclude()
        {
            try
            {
                var CategoryInclude = _Context.Categories.Include(P => P.Products).Where(P => P.CategoryID < 5).AsNoTracking().ToList();

                if (CategoryInclude is null)
                {
                    return BadRequest("Unable to display categories and their products");
                }

                return Ok(CategoryInclude);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult GetCategory(int id)
        {
            try
            {
                var category = _Context.Categories.AsNoTracking().FirstOrDefault(C => C.CategoryID == id);

                if (category is null)
                {
                    return BadRequest("Unable to find the requested category");
                }

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category is null)
                {

                    return BadRequest("Unable to create a new category");
                }

                _Context.Categories.Add(category);
                _Context.SaveChanges();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Put(int id, Category category)
        {
            try
            {
                if (id != category.CategoryID)
                {
                    return BadRequest("Please provide a valid ID.");
                }

                _Context.Entry(category).State = EntityState.Modified;
                _Context.SaveChanges();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }
    }
}
