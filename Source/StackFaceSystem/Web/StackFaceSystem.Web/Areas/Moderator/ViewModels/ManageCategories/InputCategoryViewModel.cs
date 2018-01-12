namespace StackFaceSystem.Web.Areas.Moderator.ViewModels.ManageCategories
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Mapping;
    using StackFaceSystem.Data.Models;

    public class InputCategoryViewModel : IMapTo<Category>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
