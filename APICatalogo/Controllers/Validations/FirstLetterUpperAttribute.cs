using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Controllers.Validations
{
    public class FirstLetterUpperAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("The first letter of the product name must be upper case.");
            }

            return ValidationResult.Success;
        }
    }
}
