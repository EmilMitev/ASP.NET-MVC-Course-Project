namespace StackFaceSystem.Web.CustomAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ExcludeChar : ValidationAttribute
    {
        private readonly string m_Chars;

        public ExcludeChar(string chars, string error)
            : base(error)
        {
            m_Chars = chars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                for (int i = 0; i < m_Chars.Length; i++)
                {
                    var valueAsString = value.ToString();
                    if (valueAsString.Contains(m_Chars[i]))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        return new ValidationResult(errorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
