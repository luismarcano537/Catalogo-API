using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface ISupplierRepository
{
    IEnumerable<Supplier> Get();
    Supplier GetByID(int id);
    IEnumerable<Supplier> GetInclude();
    Supplier Create(Supplier supplier);
    bool Update(Supplier supplier);
    Supplier Delete(int id);
}
