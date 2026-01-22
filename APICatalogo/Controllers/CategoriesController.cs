using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Filters;
using APICatalogo.Repositories;

namespace APICatalogo.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _Repository;
        private readonly ILogger _Logger;

        public CategoriesController(ICategoryRepository Repository, ILogger<CategoriesController> logger)
        {
            _Repository = Repository;
            _Logger = logger;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> Get()
        {
            _Logger.LogInformation("================ GetCategory ====================");
            var categories = _Repository.GetCategories();

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
            var categoriesIncluded = _Repository.GetInclude();

            if (categoriesIncluded is null)
            {
                return BadRequest("Unable to display categories and their products");
            }

            return Ok(categoriesIncluded);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult GetCategory(int id)
        {
            _Logger.LogInformation("================ GetCategoryID ====================");
            var category = _Repository.GetByID(id);

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
                _Logger.LogWarning($"Invalid data");
                return BadRequest("Unable to create a new category");
            }

            var CategoryCreated = _Repository.Create(category);

            return new CreatedAtRouteResult("GetCategory", new { id = CategoryCreated.CategoryID }, CategoryCreated);
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

            _Repository.Update(category);

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            _Logger.LogInformation("================ DeleteCategory ====================");

            var category = _Repository.GetByID(id);

            if (category is null)
            {
                _Logger.LogWarning($"Unable to find category by id: {id}");
                return BadRequest($"Unable to find category by id: {id}");
            }

            var CategoryRemoved = _Repository.Delete(id);

            return Ok(CategoryRemoved);
        }
    }
}
