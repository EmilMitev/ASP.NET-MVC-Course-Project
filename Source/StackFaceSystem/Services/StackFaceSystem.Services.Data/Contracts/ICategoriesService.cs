namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface ICategoriesService
    {
        Category GetCategory(string name);

        IQueryable<Category> GetAllCategories();
    }
}