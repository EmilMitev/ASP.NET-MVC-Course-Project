namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> GetAllCategories();
    }
}