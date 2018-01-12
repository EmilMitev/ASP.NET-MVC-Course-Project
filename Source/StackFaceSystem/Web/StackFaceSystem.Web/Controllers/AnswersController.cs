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
        private readonly IAnswersService m_Answers;
        private readonly ICommentsService m_Comments;

        public AnswersController(IAnswersService answers, ICommentsService comments)
        {
            m_Comments = comments;
            m_Answers = answers;
        }

        [HttpGet]
        public ActionResult CreateAnswer(int postId)
        {
            var inputModel = new InputAnswerViewModel
            {
                PostId = postId
            };

            return PartialView("_CreateAnswerPartial", inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnswer(InputAnswerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotificationError"] = "Sorry but something wrong. Please try angain later and don't forget content on answer";
                return Redirect(Request.UrlReferrer.ToString());
            }

            var userId = User.Identity.GetUserId();

            var answer = new Answer
            {
                Content = model.Content,
                UserId = userId,
                PostId = model.PostId
            };

            m_Answers.CreateAnswer(answer);

            TempData["Notification"] = "You successfully answer on post.";

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult EditAnswer(int answerId)
        {
            var answerFromDb = m_Answers.GetById(answerId);
            var answer = Mapper.Map<EditAnswerViewModel>(answerFromDb);
            return PartialView("_EditAnswer", answer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAnswer(EditAnswerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            m_Answers.UpdateAnswer(model.Id, model.Content);

            TempData["Notification"] = "You successfully update your answer.";
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int answerId)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotificationError"] = "Something get wrong. Try anaing later.";
                return Redirect($"/Posts/Index");
            }

            if (Request.IsAjaxRequest())
            {
                var answer = m_Answers.GetById(answerId);

                // delete comments on  answer
                m_Comments.DeleteCommentByAnswerId(answerId);

                m_Answers.DeleteAnswer(answer);

                return Json(new { notification = "You successfully delete asnwer." });
            }

            // Don't work in ajax!!!
            return Redirect("/Posts/Index");
        }
    }
}