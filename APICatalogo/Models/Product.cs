using APICatalogo.Controllers.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Products")]
public class Product : IValidatableObject
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    //[FirstLetterUpper]
    [StringLength(80)]
    public string? Name { get; set; }

    [Required]
    [StringLength(300)]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageURL { get; set; }
    public float Stock { get; set; }
    public DateTime DateRegistration { get; set; }

    public int CategoryID { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
    public int? SupplierID { get; set; }
    [JsonIgnore]
    public Supplier? Supplier { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Name))
        {
            var firstLetter = this.Name[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                yield return new ValidationResult("The first letter of the product name must be upper case", new[] { nameof(this.Name) });
            }
        }
    }
}
