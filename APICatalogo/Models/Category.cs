using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
    }

    public int CategoryID { get; set; }
    public string? Name { get; set; }
    public string? ImageURL { get; set; }

    public ICollection<Product>? Products { get; set; }
}
