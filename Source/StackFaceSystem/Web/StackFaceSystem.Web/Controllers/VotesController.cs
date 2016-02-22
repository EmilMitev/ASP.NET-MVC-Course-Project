namespace StackFaceSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Services.Data;

    public class VotesController : BaseController
    {
        private readonly IVotesService votes;

        public VotesController(IVotesService votes)
        {
            this.votes = votes;
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

            var userId = this.User.Identity.GetUserId();

            var votesCount = this.votes.RegisterVote(userId, subjectType, subjectId, voteType);

            return this.Json(new { Count = votesCount });
        }
    }
}