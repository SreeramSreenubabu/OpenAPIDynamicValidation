using OpenAPIDynamicValidation.DAL;
using OpenAPIDynamicValidation.DAL.Models;
using System.Collections.Generic;

namespace OpenAPIDynamicValidation.BAL
{
    public class ValidationService
    {
        public List<string> ValidateModel(ValidationModel model)
        {
            var validationMessages = ValidationRuleProvider.GetValidationMessages(model);
            var validationResults = new List<string>();

            // Collecting error messages from validation rules
            foreach (var rule in validationMessages)
            {
                validationResults.Add(rule.Value); // Add the error message if exists
            }

            return validationResults;
        }
    }
}
