namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IUsersService
    {
        IQueryable<ApplicationUser> All();

        ApplicationUser GetById(string id);
    }
}