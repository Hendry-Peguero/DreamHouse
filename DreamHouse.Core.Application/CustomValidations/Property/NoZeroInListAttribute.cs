using System.ComponentModel.DataAnnotations;

namespace DreamHouse.Core.Application.CustomValidations.Property
{

    public class NoZeroInListAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as List<int>;

            if (list != null && list.Contains(0)) return new ValidationResult(ErrorMessage ?? "The list cannot contain zero.");
            return ValidationResult.Success;
        }
    }
}
