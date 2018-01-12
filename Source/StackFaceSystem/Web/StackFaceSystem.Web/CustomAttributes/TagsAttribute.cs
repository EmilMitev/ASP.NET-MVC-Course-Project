namespace StackFaceSystem.Web.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TagsAttribute : ValidationAttribute
    {
        private readonly int m_Length;

        public TagsAttribute(int length, string error)
            : base(error)
        {
            m_Length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var tagsAsString = value.ToString();
                var tags = tagsAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tags.Length > 7)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }

                foreach (var tag in tags)
                {
                    if (tag.Length > m_Length)
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
