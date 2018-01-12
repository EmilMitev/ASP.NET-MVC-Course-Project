namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<Category> m_Categories;

        public CategoriesService(IDbRepository<Category> categories)
        {
            m_Categories = categories;
        }

        public void CreateCategory(Category category)
        {
            m_Categories.Add(category);
            m_Categories.Save();
        }

        public Category GetById(int id)
        {
            return m_Categories.GetById(id);
        }

        public Category GetCategory(string name)
        {
            return m_Categories
                        .All()
                        .FirstOrDefault(x => x.Name == name);
        }

        public IQueryable<Category> GetAllCategories()
        {
            return m_Categories
                        .All()
                        .OrderBy(x => x.Name);
        }

        public IQueryable<Category> GetCategoriesByPageAndSort(string sortType, string sortDirection, int page, int take)
        {
            IQueryable<Category> categories = null;
            switch (sortType)
            {
                case "Id":
                    categories = sortDirection == "ascending" ? m_Categories.All().OrderBy(x => x.Id) : m_Categories.All().OrderByDescending(x => x.Id);
                    break;
                case "Name":
                    categories = sortDirection == "ascending" ? m_Categories.All().OrderBy(x => x.Name) : m_Categories.All().OrderByDescending(x => x.Name);
                    break;
                case "CreatedOn":
                    categories = sortDirection == "ascending" ? m_Categories.All().OrderBy(x => x.CreatedOn) : m_Categories.All().OrderByDescending(x => x.CreatedOn);
                    break;
                case "ModifiedOn":
                    categories = sortDirection == "ascending" ? m_Categories.All().OrderBy(x => x.ModifiedOn) : m_Categories.All().OrderByDescending(x => x.ModifiedOn);
                    break;
            }

            return categories?
                        .Skip((page - 1) * take)
                        .Take(take);
        }

        public int GetAllCategoriesCount()
        {
            return m_Categories
                        .All()
                        .Count();
        }

        public void UpdateCategory(int categoryId, string name)
        {
            var category = m_Categories.GetById(categoryId);
            category.Name = name;
            m_Categories.Save();
        }

        public void DeleteCategory(Category category)
        {
            m_Categories.Delete(category);
            m_Categories.Save();
        }
    }
}
