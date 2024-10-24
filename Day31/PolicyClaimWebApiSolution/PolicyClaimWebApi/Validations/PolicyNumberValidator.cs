using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Validations
{
    public class PolicyNumberValidator:ValidationAttribute
    {
        protected override ValidationResult isValid(object value, ValidationContext validationContext)
        {
            if (value == null) {
                return new ValidationResult("Policy Number cannot be empty");
            }
            return ValidationResult.Success;
        }
    }
}
