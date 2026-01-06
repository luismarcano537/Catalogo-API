using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [StringLength(80)]
    public string? Name { get; set; }

    [Required]
    [StringLength(300)]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageURL { get; set; }
    public float Stock { get; set; }
    public DateTime DateRegistration { get; set; }

    public int CategoryID { get; set; }
    public Category? Category { get; set; }
}
