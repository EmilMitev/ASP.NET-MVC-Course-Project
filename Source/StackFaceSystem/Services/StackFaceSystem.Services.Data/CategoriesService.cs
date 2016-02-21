namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<Category> categories;

        public CategoriesService(IDbRepository<Category> categories)
        {
            this.categories = categories;
        }

        public IQueryable<Category> GetAllCategories()
        {
            var categories = this.categories
                            .All()
                            .OrderBy(x => x.Name);
            return categories;
        }
    }
}
