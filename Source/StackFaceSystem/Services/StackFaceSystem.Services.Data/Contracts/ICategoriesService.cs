namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface ICategoriesService
    {
        void CreateCategory(Category category);

        Category GetCategory(string name);

        IQueryable<Category> GetAllCategories();

        IQueryable<Category> GetCategoriesByPageAndSort(string sortType, string sortDirection, int page, int take);

        int GetAllCategoriesCount();

        void UpdateCategory(int categoryId, string name);

        void DeleteCategory(int categoryId);
    }
}