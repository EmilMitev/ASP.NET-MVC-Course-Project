namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using StackFaceSystem.Data.Models;
    using ViewModels.Comments;

    [Authorize]
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
            return this.PartialView("_CreateCommentPartial", inputModel);
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

        [HttpGet]
        public ActionResult EditComment(int commentId)
        {
            var commentFromDb = this.comments.GetById(commentId);
            var comment = this.Mapper.Map<EditCommentViewModel>(commentFromDb);
            return this.PartialView("_EditCommentPartial", comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(EditCommentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.comments.UpdateComment(model.Id, model.Content);

            this.TempData["Notification"] = "You successfully update your comment.";
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["NotificationError"] = "Something get wrong. Try anaing later.";
                return this.Redirect($"/Posts/Index");
            }

            if (this.Request.IsAjaxRequest())
            {
                var comment = this.comments.GetById(commentId);
                this.comments.DeleteComment(comment);

                return this.Json(new { notification = "You successfully delete comment." });
            }

            // Don't work in ajax!!!
            return this.Redirect("/Posts/Index");
        }
    }
}