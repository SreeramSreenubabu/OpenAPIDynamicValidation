using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenAPIDynamicValidation.DAL
{
    public static class ValidationRuleProvider
    {
        public static Dictionary<string, string> GetValidationMessages(object model)
        {
            var validationMessages = new Dictionary<string, string>();

            foreach (var property in model.GetType().GetProperties())
            {
                var propertyName = property.Name;
                var propertyType = property.PropertyType;

                if (propertyType == typeof(string))
                {
                    if (propertyName == "RequestedID")
                    {
                        var validationResult = ValidateRequestedID(property.GetValue(model));
                        if (!validationResult.isValid)
                        {
                            validationMessages[propertyName] = validationResult.errorMessage;
                        }
                    }
                    else if (propertyName == "SecurityCode")
                    {
                        var validationResult = ValidateSecurityCode(property.GetValue(model));
                        if (!validationResult.isValid)
                        {
                            validationMessages[propertyName] = validationResult.errorMessage;
                        }
                    }
                }
                else if (propertyType == typeof(DateTime))
                {
                    if (propertyName == "RequestedOn")
                    {
                        var validationResult = ValidateRequestedOn(property.GetValue(model));
                        if (!validationResult.isValid)
                        {
                            validationMessages[propertyName] = validationResult.errorMessage;
                        }
                    }
                }
                else if (propertyType == typeof(int?))
                {
                    if (propertyName == "ExtraRowCount")
                    {
                        var validationResult = ValidateExtraRowCount(property.GetValue(model));
                        if (!validationResult.isValid)
                        {
                            validationMessages[propertyName] = validationResult.errorMessage;
                        }
                    }
                }
            }

            return validationMessages;
        }

        // Validation for RequestedID
        private static (bool isValid, string errorMessage) ValidateRequestedID(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return (false, "RequestedID is required and cannot be empty.");

            string str = value.ToString();

            if (str.Length > 100)
                return (false, "RequestedID must be up to 100 characters long.");

            if (!Regex.IsMatch(str, @"^[a-zA-Z0-9]+$"))
                return (false, "RequestedID must be alphanumeric and cannot contain special characters.");

            return (true, string.Empty); // Valid
        }

        // Validation for SecurityCode
        private static (bool isValid, string errorMessage) ValidateSecurityCode(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return (false, "SecurityCode is required.");

            string str = value.ToString();

            if (str.Length != 6)
                return (false, "SecurityCode must be exactly 6 characters long.");           

            if (!Regex.IsMatch(str, @"^[a-zA-Z0-9]+$"))
                return (false, "SecurityCode must be alphanumeric and cannot contain special characters.");

            return (true, string.Empty); // Valid
        }

        // Validation for RequestedOn
        private static (bool isValid, string errorMessage) ValidateRequestedOn(object value)
        {
            // Check if the value is null or empty
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return (false, "RequestedOn is required.");
            }

            // Try to parse the value as a DateTime
            if (!DateTime.TryParse(value.ToString(), out DateTime date))
            {
                return (false, "RequestedOn must be a valid date in the correct format.");
            }

            // Check if the date is in the future
            if (date > DateTime.UtcNow)
            {
                return (false, "RequestedOn cannot be in the future.");
            }

            // Check if the date is from yesterday or earlier
            if (date < DateTime.UtcNow.Date)
            {
                return (false, "RequestedOn cannot be a past date (yesterday or earlier).");
            }

            return (true, string.Empty); // Valid
        }



        // Validation for ExtraRowCount
        private static (bool isValid, string errorMessage) ValidateExtraRowCount(object value)
        {
            // If the value is null, it's valid (because ExtraRowCount is nullable)
            if (value == null)
                return (true, string.Empty);

            string valueStr = value.ToString();

            // Check if the value contains only digits and no alphabets or special characters
            if (!Regex.IsMatch(valueStr, @"^\d+$"))
            {
                return (false, "ExtraRowCount must be a valid numeric value without alphabets or special characters.");
            }

            // Try to parse the value to an integer
            if (int.TryParse(valueStr, out int result))
            {
                // Check if the result is either 0 or 20
                if (result == 0 || result == 20)
                    return (true, string.Empty); // Valid
            }

            // If the value is neither 0 nor 20, return an error
            return (false, "ExtraRowCount must be 0 or 20.");
        }


    }
}
