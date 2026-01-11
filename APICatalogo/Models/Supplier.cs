using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Suppliers")]
public class Supplier
{
    public Supplier()
    {
        Products = new Collection<Product>();
    }

    [Key]
    public int SupplierID { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; }

    [Required]
    [StringLength(80)]
    public string Document { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    [StringLength(300)]
    public string ImageURL { get; set; }
    public DateTime DateCreation { get; set; }

    public ICollection<Product> Products { get; set; }
}
