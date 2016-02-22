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
        public ActionResult CreateComment()
        {
            // var url = this.Request.UrlReferrer.PathAndQuery;
            // var postId = url.Substring(url.LastIndexOf('/') + 1);
            // var inputModel = new InputAnswerViewModel
            // {
            //    PostId = postId
            // };
            return this.PartialView("_CreateComment");
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

            var answer = new Comment
            {
                Content = model.Content,
                UserId = userId
            };

            this.comments.CreateComment("somestring", answer);

            this.TempData["Notification"] = "You successfully add your post.";

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }
    }
}