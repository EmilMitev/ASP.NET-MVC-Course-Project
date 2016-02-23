namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using StackFaceSystem.Data.Models;
    using ViewModels.Answers;

    public class AnswersController : BaseController
    {
        private readonly IAnswersService answers;

        public AnswersController(IAnswersService answers)
        {
            this.answers = answers;
        }

        [HttpGet]
        public ActionResult CreateAnswer(int postId)
        {
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
                UserId = userId,
                PostId = model.PostId
            };

            this.answers.CreateAnswer(answer);

            this.TempData["Notification"] = "You successfully answer on post.";

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int answerId, string authorName)
        {
            // TODO: make that delete data is correct
            return null;
        }

        [HttpGet]
        public ActionResult EditAnswer(int answerId)
        {
            var answerFromDb = this.answers.GetAnswerById(answerId);
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
            // TODO: make it happend
            return null;
            //this.posts.UpdatePost(model.EncodedId, model.Title, model.Content);

            //this.TempData["Notification"] = "You successfully update your post.";
            //return this.Redirect($"/Posts/Details/{model.EncodedId}");
        }
    }
}