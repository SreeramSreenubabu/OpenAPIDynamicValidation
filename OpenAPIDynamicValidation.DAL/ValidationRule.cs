namespace OpenAPIDynamicValidation.DAL
{
    public class ValidationRule
    {
        public string FieldName { get; set; }
        public Func<object, bool> ValidationFunc { get; set; }
        public string ErrorMessage { get; set; }
    }
}
