using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public ICollection<Product> Products { get; set; }
}
