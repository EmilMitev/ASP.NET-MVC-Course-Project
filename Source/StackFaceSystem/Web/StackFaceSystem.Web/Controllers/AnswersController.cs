namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using StackFaceSystem.Data.Models;
    using ViewModels.Answers;

    [Authorize]
    public class AnswersController : BaseController
    {
        private readonly IAnswersService answers;
        private readonly ICommentsService comments;

        public AnswersController(IAnswersService answers, ICommentsService comments)
        {
            this.comments = comments;
            this.answers = answers;
        }

        [HttpGet]
        public ActionResult CreateAnswer(int postId)
        {
            var inputModel = new InputAnswerViewModel
            {
                PostId = postId
            };

            return this.PartialView("_CreateAnswerPartial", inputModel);
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
                UserId = userId,
                PostId = model.PostId
            };

            this.answers.CreateAnswer(answer);

            this.TempData["Notification"] = "You successfully answer on post.";

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult EditAnswer(int answerId)
        {
            var answerFromDb = this.answers.GetById(answerId);
            var answer = this.Mapper.Map<EditAnswerViewModel>(answerFromDb);
            return this.PartialView("_EditAnswer", answer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAnswer(EditAnswerViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.answers.UpdateAnswer(model.Id, model.Content);

            this.TempData["Notification"] = "You successfully update your answer.";
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int answerId)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["NotificationError"] = "Something get wrong. Try anaing later.";
                return this.Redirect($"/Posts/Index");
            }

            if (this.Request.IsAjaxRequest())
            {
                var answer = this.answers.GetById(answerId);

                // delete comments on this answer
                this.comments.DeleteCommentByAnswerId(answerId);

                this.answers.DeleteAnswer(answer);

                return this.Json(new { notification = "You successfully delete asnwer." });
            }

            // Don't work in ajax!!!
            return this.Redirect("/Posts/Index");
        }
    }
}