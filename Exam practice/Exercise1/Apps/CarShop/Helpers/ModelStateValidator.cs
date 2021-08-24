using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Helpers
{
    public static class ModelStateValidator
    {
        public static bool IsValid(object model)
        {
            ValidationContext validator = new ValidationContext(model);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, validator, validationResults, true);

            return isValid;
        }
    }
}
