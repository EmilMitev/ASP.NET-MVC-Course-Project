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

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.users.All().OrderBy(x => x.UserName);
        }

        public ApplicationUser GetById(string id)
        {
            return this.users.GetById(id);
        }

        public void UpdateUser(ApplicationUser user)
        {
            var userFromDb = this.users.GetById(user.Id);

            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.ImageUrl = user.ImageUrl;
            userFromDb.FacebookUrl = user.FacebookUrl;
            userFromDb.TwitterUrl = user.TwitterUrl;
            userFromDb.GoogleUrl = user.GoogleUrl;
            userFromDb.LinkedInUrl = user.LinkedInUrl;
            userFromDb.GitHubUrl = user.GitHubUrl;
            userFromDb.Adress = user.Adress;

            this.users.SaveChanges();
        }
    }
}