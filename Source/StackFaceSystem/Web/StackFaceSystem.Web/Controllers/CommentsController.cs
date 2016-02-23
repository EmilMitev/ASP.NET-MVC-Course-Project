namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using StackFaceSystem.Data.Models;
    using ViewModels.Comments;

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

        [HttpPost]
        public ActionResult DeleteComment(int commentId, string authorName)
        {
            // TODO: make that delete data is correct
            return null;
        }

        [HttpGet]
        public ActionResult EditComment(int commentId)
        {
            var commentFromDb = this.comments.GetById(commentId);
            var comment = this.Mapper.Map<EditCommentViewModel>(commentFromDb);
            return this.PartialView("_EditComment", comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(EditCommentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return null;

            //this.TempData["Notification"] = "You successfully update your post.";
            //return this.Redirect($"/Posts/Details/{model.EncodedId}");
        }
    }
}