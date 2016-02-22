namespace StackFaceSystem.Web.ViewModels.Categories
{
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
