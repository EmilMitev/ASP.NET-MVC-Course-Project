namespace StackFaceSystem.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;
    using CustomAttributes;
    using Categories;
    public class InputPostViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]
        public ICollection<CategoriesViewModel> Categories { get; set; }

        [NotMapped]
        [Required]
        [ExcludeChar(chars: "{}!@$%.^&", error: "{0}contains invalid character({}!@$%.^&).")]
        [TagsAttribute(length: 10, error: "{0} contains tag with length > 10 or you enter more than 7 tags.")]
        public string Tags { get; set; }
    }
}
