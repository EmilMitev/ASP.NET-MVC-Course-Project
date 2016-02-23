namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> GetAllCategories();

        Category GetCategory(string name);
    }
}