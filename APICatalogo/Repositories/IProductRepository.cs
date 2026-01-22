using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface IProductRepository
{
    IQueryable<Product> Get();
    Product GetByID(int id);
    Product Created(Product product);
    bool Update(Product product);
    bool Delete(int id);
}
