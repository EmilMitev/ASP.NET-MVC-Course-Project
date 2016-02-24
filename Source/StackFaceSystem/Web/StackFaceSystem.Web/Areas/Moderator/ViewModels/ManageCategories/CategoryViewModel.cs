namespace StackFaceSystem.Web.Areas.Moderator.ViewModels.ManageCategories
{
    using System;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
