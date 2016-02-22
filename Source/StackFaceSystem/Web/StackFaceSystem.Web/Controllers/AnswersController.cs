namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Contracts.Data;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Services.Data;
    using StackFaceSystem.Web.ViewModels.Posts;

    public class AnswersController : BaseController
    {
        private readonly IAnswersService answers;

        public AnswersController(IAnswersService answers)
        {
            this.answers = answers;
        }

        [HttpGet]
        public ActionResult CreateAnswer()
        {
            var url = this.Request.UrlReferrer.PathAndQuery;
            var postId = url.Substring(url.LastIndexOf('/') + 1);
            var inputModel = new InputAnswerViewModel
            {
                PostId = postId
            };

            return this.PartialView("_CreateAnswer", inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnswer(InputAnswerViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["NotificationError"] = "Sorry but something wrong. Please try angain later and don't forget content on answer";
                return this.Redirect(this.Request.UrlReferrer.ToString());
            }

            var userId = this.User.Identity.GetUserId();

            var answer = new Answer
            {
                Content = model.Content,
                UserId = userId
            };

            this.answers.CreateAnswer(model.PostId, answer);

            this.TempData["Notification"] = "You successfully add your post.";

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }
    }
}