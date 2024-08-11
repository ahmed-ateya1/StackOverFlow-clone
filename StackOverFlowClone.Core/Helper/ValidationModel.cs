using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Helper
{
    public static class ValidationModel
    {
        public static void ValidateModel(object? model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            
            ValidationContext validationContext = new ValidationContext(model);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool IsValid = Validator.
                TryValidateObject(model, validationContext, validationResults, true);

            if (!IsValid)
                throw new ArgumentException();
        }
    }
}
