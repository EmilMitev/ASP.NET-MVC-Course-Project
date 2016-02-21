namespace StackFaceSystem.Web.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TagsAttribute : ValidationAttribute
    {
        private readonly int length;

        public TagsAttribute(int length, string error)
            : base(error)
        {
            this.length = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var tagsAsString = value.ToString();
                var tags = tagsAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tag in tags)
                {
                    if (tag.Length > this.length)
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
