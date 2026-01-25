using APICatalogo.Models;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly APICatalogoContext _context;

        public SupplierRepository(APICatalogoContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> Get()
        {
            return _context.Suppliers.ToList();
        }


        public Supplier GetByID(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(S => S.SupplierID == id);

            if (supplier == null)
            {
                throw new ArgumentException($"Unable to locate Supplier with ID: {id}");
            }

            return supplier;
        }


        public IEnumerable<Supplier> GetInclude()
        {
            var supplierInclude = _context.Suppliers.Include(S => S.Products).Where(S => S.SupplierID <= 5).AsNoTracking().ToList();

            if (supplierInclude is null)
            {
                throw new ArgumentException($"The informed Supplier does not have registered products.");
            }

            return supplierInclude;
        }


        public Supplier Create(Supplier supplier)
        {
            if (supplier is null)
            {
                throw new ArgumentException("Please provide a valid supplier.");
            }

            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            return supplier;
        }


        public Supplier Update(Supplier supplier)
        {
            var supplierUpdate = _context.Suppliers.FirstOrDefault(S => S.SupplierID == supplier.SupplierID);

            if (supplierUpdate is null)
            {
                throw new ArgumentException($"Supplier with ID: {supplier.SupplierID} Not found.");
            }

            _context.Suppliers.Entry(supplier).State = EntityState.Modified;
            _context.SaveChanges();

            return supplierUpdate;
        }


        public Supplier Delete(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(S => S.SupplierID == id);

            if (supplier is null)
            {
                throw new ArgumentException($"Supplier with ID: {supplier.SupplierID} Not found.");
            }

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();

            return supplier;
        }
    }
}
