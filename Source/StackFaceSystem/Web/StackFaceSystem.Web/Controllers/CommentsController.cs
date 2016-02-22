namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Services.Contracts.Data;
    using StackFaceSystem.Web.ViewModels.Posts;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService comments;

        public CommentsController(ICommentsService comments)
        {
            this.comments = comments;
        }

        [HttpGet]
        public ActionResult CreateComment(int answerId)
        {
            var inputModel = new InputCommentViewModel
            {
                AnswerId = answerId
            };
            return this.PartialView("_CreateComment", inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(InputCommentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["NotificationError"] = "Sorry but something wrong. Please try angain later and don't forget content on comment";
                return this.Redirect(this.Request.UrlReferrer.ToString());
            }

            var userId = this.User.Identity.GetUserId();

            var comment = new Comment
            {
                Content = model.Content,
                UserId = userId,
                AnswerId = model.AnswerId
            };

            this.comments.CreateComment(comment);

            this.TempData["Notification"] = "You successfully comment.";

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }
    }
}