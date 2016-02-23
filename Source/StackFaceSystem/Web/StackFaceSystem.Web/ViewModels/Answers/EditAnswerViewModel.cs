namespace StackFaceSystem.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;

    public class EditAnswerViewModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }
    }
}
