namespace StackFaceSystem.Web.CustomAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ExcludeChar : ValidationAttribute
    {
        private readonly string chars;

        public ExcludeChar(string chars, string error)
            : base(error)
        {
            this.chars = chars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                for (int i = 0; i < this.chars.Length; i++)
                {
                    var valueAsString = value.ToString();
                    if (valueAsString.Contains(this.chars[i]))
                    {
                        var errorMessage = this.FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
