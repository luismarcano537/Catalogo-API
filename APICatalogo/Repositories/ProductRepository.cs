using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly APICatalogoContext _Context;

        public ProductRepository(APICatalogoContext context)
        {
            _Context = context;
        }

        public IQueryable<Product> Get()
        {
            return _Context.Products;
        }

        public Product GetByID(int id)
        {
            var product = _Context.Products.FirstOrDefault(P => P.ProductID == id);

            if (product is null)
            {
                throw new ArgumentException("The product is empty");
            }

            return product;
        }

        public Product Created(Product product)
        {
            if (product is null)
            {
                throw new ArgumentException("Enter a valid product.");
            }

            _Context.Products.Add(product);
            _Context.SaveChanges();

            return product;
        }

        public bool Update(Product product)
        {
            if (product is null)
            {
                throw new ArgumentException("Enter a valid product.");
            }

            if (_Context.Products.Any(P => P.ProductID == product.ProductID))
            {
                _Context.Products.Update(product);
                _Context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var product = _Context.Products.Find(id);

            if (product is null)
            {
                return false;
            }

            _Context.Products.Remove(product);
            _Context.SaveChanges();

            return true;
        }
    }
}
