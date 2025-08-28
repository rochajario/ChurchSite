using System.ComponentModel.DataAnnotations;

namespace Church.Domain.Models
{
    public abstract class BaseModel
    {
        public (bool IsValid, List<ValidationResult> Errors) VerboseValidation()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                this,
                context,
                results,
                validateAllProperties: true
            );

            return (isValid, results);
        }

        public bool SimpleValidation()
        {
            var context = new ValidationContext(this);

            bool isValid = Validator.TryValidateObject(
                this,
                context,
                null,
                validateAllProperties: true
            );

            return isValid;
        }
    }
}
