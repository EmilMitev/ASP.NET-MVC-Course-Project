namespace StackFaceSystem.Web.Areas.Moderator.ViewModels.ManageCategories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Infrastructure.Mapping;
    using StackFaceSystem.Data.Models;

    public class InputCategoryViewModel : IMapTo<Category>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
