namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IUserDbRepository<ApplicationUser> m_Users;

        public UsersService(IUserDbRepository<ApplicationUser> users)
        {
            m_Users = users;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return m_Users
                    .All()
                    .OrderBy(x => x.UserName);
        }

        public ApplicationUser GetById(string id)
        {
            return m_Users.GetById(id);
        }

        public void UpdateUser(ApplicationUser user)
        {
            var userFromDb = m_Users.GetById(user.Id);

            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.ImageUrl = user.ImageUrl;
            userFromDb.FacebookUrl = user.FacebookUrl;
            userFromDb.TwitterUrl = user.TwitterUrl;
            userFromDb.GoogleUrl = user.GoogleUrl;
            userFromDb.LinkedInUrl = user.LinkedInUrl;
            userFromDb.GitHubUrl = user.GitHubUrl;
            userFromDb.Adress = user.Adress;

            m_Users.SaveChanges();
        }
    }
}