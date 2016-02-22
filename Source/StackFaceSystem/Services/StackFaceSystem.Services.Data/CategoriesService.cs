namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts.Data;
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

        public Category GetCategory(string name)
        {
            var categories = this.categories
                            .All()
                            .FirstOrDefault(x => x.Name == name);
            return categories;
        }
    }
}
