namespace StackFaceSystem.Web.Areas.User.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Services.Data.Contracts;
    using ViewModels.Profile;
    using Web.Controllers;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUsersService users;

        public ProfileController(IUsersService users)
        {
            this.users = users;
        }

        public enum ManageMessageId
        {
            EditProfileSuccess,
            ChangePasswordSuccess,
            SetPasswordSuccess,
            Error
        }

        // GET: /Profile/Index
        public ActionResult Index(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.EditProfileSuccess ? "Your profile has been update."
                : message == ManageMessageId.Error ? "An error has occurred."
                : string.Empty;

            var userId = this.User.Identity.GetUserId();
            var user = this.users.GetById(userId);
            var model = this.Mapper.Map<IndexViewModel>(user);

            return this.View(model);
        }

        // GET: /Profile/Edit
        public ActionResult Edit()
        {
            return this.View();
        }

        // POST: /Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChangePasswordViewModel model)
        {
            return this.View(model);
        }

        // GET: /Profile/ChangePassword
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        // POST: /Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(model);
            //}

            //var result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            //if (result.Succeeded)
            //{
            //    var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
            //    if (user != null)
            //    {
            //        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //    }

            //    return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            //}

            //this.AddErrors(result);
            return this.View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}