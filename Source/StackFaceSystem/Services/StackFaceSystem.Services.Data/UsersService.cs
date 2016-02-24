namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IUserDbRepository<ApplicationUser> users;

        public UsersService(IUserDbRepository<ApplicationUser> users)
        {
            this.users = users;
        }

        public IQueryable<ApplicationUser> All()
        {
            return this.users
                .All()
                .Where(u => !u.Roles.Select(y => y.UserId).Contains(string.Empty));
        }

        public ApplicationUser GetById(string id)
        {
            return this.users.GetById(id);
        }
    }
}