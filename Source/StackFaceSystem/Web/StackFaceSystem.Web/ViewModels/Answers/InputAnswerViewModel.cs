namespace StackFaceSystem.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class InputAnswerViewModel
    {
        public int PostId { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }
    }
}
