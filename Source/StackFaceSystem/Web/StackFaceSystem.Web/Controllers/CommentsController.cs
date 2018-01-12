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
        private readonly ICommentsService m_Comments;

        public CommentsController(ICommentsService comments)
        {
            m_Comments = comments;
        }

        [HttpGet]
        public ActionResult CreateComment(int answerId)
        {
            var inputModel = new InputCommentViewModel
            {
                AnswerId = answerId
            };
            return PartialView("_CreateCommentPartial", inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(InputCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotificationError"] = "Sorry but something wrong. Please try angain later and don't forget content on comment";
                return Redirect(Request.UrlReferrer.ToString());
            }

            var userId = User.Identity.GetUserId();

            var comment = new Comment
            {
                Content = model.Content,
                UserId = userId,
                AnswerId = model.AnswerId
            };

            m_Comments.CreateComment(comment);

            TempData["Notification"] = "You successfully comment.";

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult EditComment(int commentId)
        {
            var commentFromDb = m_Comments.GetById(commentId);
            var comment = Mapper.Map<EditCommentViewModel>(commentFromDb);
            return PartialView("_EditCommentPartial", comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(EditCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            m_Comments.UpdateComment(model.Id, model.Content);

            TempData["Notification"] = "You successfully update your comment.";
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotificationError"] = "Something get wrong. Try anaing later.";
                return Redirect($"/Posts/Index");
            }

            if (Request.IsAjaxRequest())
            {
                var comment = m_Comments.GetById(commentId);
                m_Comments.DeleteComment(comment);

                return Json(new { notification = "You successfully delete comment." });
            }

            // Don't work in ajax!!!
            return Redirect("/Posts/Index");
        }
    }
}