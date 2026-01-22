using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    Category GetByID(int id);
    IEnumerable<Category> GetInclude();
    Category Create(Category category);
    Category Update(Category category);
    Category Delete(int id);
}
