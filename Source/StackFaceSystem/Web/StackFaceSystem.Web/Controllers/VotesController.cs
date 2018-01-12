namespace StackFaceSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;

    [Authorize]
    public class VotesController : BaseController
    {
        private readonly IVotesService m_Votes;

        public VotesController(IVotesService votes)
        {
            m_Votes = votes;
        }

        [HttpPost]
        public ActionResult Vote(int subjectId, int voteType, string subjectType)
        {
            if (voteType > 1)
            {
                voteType = 1;
            }

            if (voteType < -1)
            {
                voteType = -1;
            }

            var userId = User.Identity.GetUserId();

            var votesCount = m_Votes.RegisterVote(userId, subjectType, subjectId, voteType);

            return Json(new { Count = votesCount });
        }
    }
}