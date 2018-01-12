namespace StackFaceSystem.Web.Areas.User.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Contracts;
    using ViewModels.Profile;
    using Web.Controllers;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUsersService m_Users;

        private ApplicationSignInManager m_SignInManager;
        private ApplicationUserManager m_UserManager;

        public ProfileController(IUsersService users)
        {
            m_Users = users;
        }

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public enum ManageMessageId
        {
            EditProfileSuccess,
            ChangePasswordSuccess,
            SetPasswordSuccess,
            Error
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return m_SignInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                m_SignInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return m_UserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                m_UserManager = value;
            }
        }

        // GET: /Profile/Index
        [HttpGet]
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
               message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
               : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
               : message == ManageMessageId.EditProfileSuccess ? "Your profile has been update."
               : message == ManageMessageId.Error ? "An error has occurred."
               : string.Empty;

            var userId = User.Identity.GetUserId();
            var user = m_Users.GetById(userId);
            var model = Mapper.Map<IndexViewModel>(user);

            return View(model);
        }

        // GET: /Profile/Edit
        [HttpGet]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var user = m_Users.GetById(userId);
            var model = Mapper.Map<EditProfileViewModel>(user);
            return View(model);
        }

        // POST: /Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Mapper.Map<ApplicationUser>(model);
            user.Id = User.Identity.GetUserId();
            m_Users.UpdateUser(user);

            return RedirectToAction("Index", new { Message = ManageMessageId.EditProfileSuccess });
        }

        // GET: /Profile/ChangePassword
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            AddErrors(result);
            return View(model);
        }

        // GET: /Profile/GetUser?userId=...
        [HttpGet]
        public ActionResult GetUser(string userId)
        {
            var user = m_Users.GetById(userId);
            var model = Mapper.Map<IndexViewModel>(user);
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}