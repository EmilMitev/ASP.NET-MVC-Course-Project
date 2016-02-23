namespace StackFaceSystem.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Web.Infrastructure.Mapping;

    public class EditCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }
    }
}
