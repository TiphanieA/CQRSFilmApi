using System.ComponentModel.DataAnnotations;

namespace CQRS.Shared.Attributes;

public class GuidNotEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        if (value is Guid guid && guid == Guid.Empty)
        {
            return new ValidationResult(ErrorMessage);
        }
        
        return ValidationResult.Success;
    }
}