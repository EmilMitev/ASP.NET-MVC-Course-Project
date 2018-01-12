namespace StackFaceSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Services.Data.Contracts;
    using ViewModels.Users;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class UsersController : BaseController
    {
        private readonly IUsersService m_Users;
        private readonly UserManager<ApplicationUser> m_UserManager;

        public UsersController(IUsersService users)
        {
            m_Users = users;
            m_UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var users = m_Users.GetAll().To<UserViewModel>().ToList();
            foreach (var user in users)
            {
                user.Role = m_UserManager.GetRoles(user.Id).FirstOrDefault();
            }

            var viewModel = new IndexViewModel
            {
                Users = users
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult EditUserRole(string id)
        {
            var userFromDb = m_Users.GetById(id);
            var user = Mapper.Map<UserViewModel>(userFromDb);
            user.Role = m_UserManager.GetRoles(user.Id).FirstOrDefault();

            return PartialView("_UserDetailsPartial", user);
        }

        [HttpPost]
        public ActionResult ChangeRole(string userId, string role)
        {
            var rolesForUser = m_UserManager.GetRoles(userId).ToList();

            rolesForUser.ForEach(r => m_UserManager.RemoveFromRole(userId, r));

            m_UserManager.AddToRole(userId, role);

            return RedirectToAction("Index");
        }
    }
}