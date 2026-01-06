using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    public class APICatalogoContext : DbContext
    {
        public APICatalogoContext(DbContextOptions<APICatalogoContext> options ) : base(options) { }

        DbSet<Category>? Categories { get; set; }
        DbSet<Product>? Products { get; set; }
    }
}
