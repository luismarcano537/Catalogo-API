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
            var supplierInclude = _context.Suppliers.Include(S => S.Products).AsNoTracking().ToList();

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


        public bool Update(Supplier supplier)
        {
            if (supplier is null)
            {
                throw new ArgumentException("Please provide a valid supplier.");
            }

            if (_context.Suppliers.Any(S => S.SupplierID == supplier.SupplierID))
            {
                _context.Suppliers.Update(supplier);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
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
