using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly APICatalogoContext _Context;

    public CategoryRepository(APICatalogoContext context)
    {
        _Context = context;
    }

    public IEnumerable<Category> GetCategories()
    {
        var categories = _Context.Categories.AsNoTracking().ToList();

        return categories;
    }

    public Category GetByID(int id)
    {
        var category = _Context.Categories.FirstOrDefault(C => C.CategoryID == id);

        return category;
    }

    public IEnumerable<Category> GetInclude()
    {
        var CategoryInclude = _Context.Categories.Include(P => P.Products).Where(P => P.CategoryID < 5).AsNoTracking().ToList();

        return CategoryInclude;
    }

    public Category Create(Category category)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        _Context.Categories.Add(category);
        _Context.SaveChanges();
        return category;
    }
    public Category Update(Category category)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        _Context.Entry(category).State = EntityState.Modified;
        _Context.SaveChanges();
        return category;
    }

    public Category Delete(int id)
    {
        var category = _Context.Categories.Find(id);

        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        _Context.Remove(category);
        _Context.SaveChanges();

        return category;
    }
}
