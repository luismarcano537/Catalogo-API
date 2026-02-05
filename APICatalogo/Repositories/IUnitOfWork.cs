using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Supplier> SupplierRepository { get; }

        void Commit();
    }
}
