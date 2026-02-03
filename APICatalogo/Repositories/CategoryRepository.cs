using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
   public CategoryRepository(APICatalogoContext context) : base(context)
    {
    }
}
