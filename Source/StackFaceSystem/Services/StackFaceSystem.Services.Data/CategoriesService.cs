namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<Category> categories;

        public CategoriesService(IDbRepository<Category> categories)
        {
            this.categories = categories;
        }

        public void CreateCategory(Category category)
        {
            this.categories.Add(category);
            this.categories.Save();
        }

        public Category GetCategory(string name)
        {
            var categories = this.categories
                            .All()
                            .FirstOrDefault(x => x.Name == name);
            return categories;
        }

        public IQueryable<Category> GetAllCategories()
        {
            var categories = this.categories
                            .All()
                            .OrderBy(x => x.Name);
            return categories;
        }

        public IQueryable<Category> GetCategoriesByPageAndSort(string sortType, string sortDirection, int page, int take)
        {
            IQueryable<Category> categories = null;
            switch (sortType)
            {
                case "Id":
                    categories = sortDirection == "ascending" ? this.categories.All().OrderBy(x => x.Id) : this.categories.All().OrderByDescending(x => x.Id);
                    break;
                case "Name":
                    categories = sortDirection == "ascending" ? this.categories.All().OrderBy(x => x.Name) : this.categories.All().OrderByDescending(x => x.Name);
                    break;
                case "CreatedOn":
                    categories = sortDirection == "ascending" ? this.categories.All().OrderBy(x => x.CreatedOn) : this.categories.All().OrderByDescending(x => x.CreatedOn);
                    break;
                case "ModifiedOn":
                    categories = sortDirection == "ascending" ? this.categories.All().OrderBy(x => x.ModifiedOn) : this.categories.All().OrderByDescending(x => x.ModifiedOn);
                    break;
            }

            categories = categories
                           .Skip((page - 1) * take)
                           .Take(take);

            return categories;
        }

        public int GetAllCategoriesCount()
        {
            var number = this.categories
                            .All()
                            .Count();
            return number;
        }

        public void UpdateCategory(int categoryId, string name)
        {
            var category = this.categories.GetById(categoryId);
            category.Name = name;
            this.categories.Save();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = this.categories.GetById(categoryId);
            this.categories.Delete(category);
            this.categories.Save();
        }
    }
}
