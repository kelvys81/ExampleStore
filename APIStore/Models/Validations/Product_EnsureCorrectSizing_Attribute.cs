using System.ComponentModel.DataAnnotations;

namespace APIStore.Models.Validations
{
    public class Product_EnsureCorrectSizing_Attribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var prod = validationContext.ObjectInstance as Product;
            if (prod != null && !string.IsNullOrWhiteSpace(prod.Gender))
            {
                if (prod.Gender.Equals("men", StringComparison.OrdinalIgnoreCase) && prod.Size < 8)
                {
                    return new ValidationResult("For men`s product, the size has to be greater or iqual to 8.");
                }
                else if (prod.Gender.Equals("women", StringComparison.OrdinalIgnoreCase) && prod.Size < 6)
                {
                    return new ValidationResult("For men`s product, the size has to be greater or iqual to 8.");
                }

            }
            return ValidationResult.Success;
        }

    }
}
