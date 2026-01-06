using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Supplier
{
    public Supplier()
    {
        Products = new Collection<Product>();
    }

    public int SupplierID { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public bool IsActive { get; set; } = true;
    public string ImageURL { get; set; }
    public DateTime DateCreation { get; set; }

    public ICollection<Product> Products { get; set; }
}
