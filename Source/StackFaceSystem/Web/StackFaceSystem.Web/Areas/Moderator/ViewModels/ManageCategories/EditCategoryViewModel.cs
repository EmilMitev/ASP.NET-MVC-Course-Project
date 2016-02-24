namespace StackFaceSystem.Web.Areas.Moderator.ViewModels.ManageCategories
{
    using System.ComponentModel.DataAnnotations;

    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
