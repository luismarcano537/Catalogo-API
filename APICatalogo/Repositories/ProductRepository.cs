using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(APICatalogoContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetProductsByCategory(int id)
        {
            return Get().Where(C => C.CategoryID == id);
        }

        public IEnumerable<Product> GetProductsBySupplier(int id)
        {
            return Get().Where(S => S.SupplierID == id);
        }
    }
}
