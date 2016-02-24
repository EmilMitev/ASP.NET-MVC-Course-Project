namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IUsersService
    {
        IQueryable<ApplicationUser> GetAll();

        ApplicationUser GetById(string id);

        void UpdateUser(ApplicationUser user);
    }
}