namespace StackFaceSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using Data;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Contracts;
    using ViewModels.Users;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class UsersController : BaseController
    {
        private readonly IUsersService users;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService users)
        {
            this.users = users;
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        [HttpGet]
        public ActionResult Index()
        {
            var users = this.users.GetAll().To<UserViewModel>().ToList();
            foreach (var user in users)
            {
                user.Role = this.userManager.GetRoles(user.Id).FirstOrDefault();
            }

            var viewModel = new IndexViewModel
            {
                Users = users
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult EditUserRole(string id)
        {
            var userFromDb = this.users.GetById(id);
            var user = this.Mapper.Map<UserViewModel>(userFromDb);
            user.Role = this.userManager.GetRoles(user.Id).FirstOrDefault();

            return this.PartialView("_UserDetailsPartial", user);
        }

        [HttpPost]
        public ActionResult ChangeRole(string userId, string role)
        {
            var rolesForUser = this.userManager.GetRoles(userId).ToList();

            rolesForUser.ForEach(r => this.userManager.RemoveFromRole(userId, r));

            this.userManager.AddToRole(userId, role);

            return this.RedirectToAction("Index");
        }
    }
}