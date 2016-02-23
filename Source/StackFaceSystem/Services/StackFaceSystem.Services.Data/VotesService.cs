namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IDbRepository<Vote> votes;

        public VotesService(IDbRepository<Vote> votes)
        {
            this.votes = votes;
        }

        public int RegisterVote(string userId, string subject, int id, int voteType)
        {
            Vote vote;

            if (subject == "post")
            {
                vote = this.votes.All().FirstOrDefault(x => x.UserId == userId && x.PostId == id);
                if (vote == null)
                {
                    vote = new Vote
                    {
                        UserId = userId,
                        PostId = id,
                        Value = (VoteValue)voteType
                    };
                    this.votes.Add(vote);
                }
                else
                {
                    if (vote.Value == (VoteValue)voteType)
                    {
                        vote.Value = VoteValue.Neutral;
                    }
                    else if (vote.Value != (VoteValue)voteType)
                    {
                        vote.Value = (VoteValue)voteType;
                    }
                }

                this.votes.Save();

                return this.votes.All()
                                    .Where(x => x.PostId == id)
                                    .Sum(x => (int)x.Value);
            }
            else if (subject == "answer")
            {
                vote = this.votes.All().FirstOrDefault(x => x.UserId == userId && x.AnswerId == id);
                if (vote == null)
                {
                    vote = new Vote
                    {
                        UserId = userId,
                        AnswerId = id,
                        Value = (VoteValue)voteType
                    };
                    this.votes.Add(vote);
                }
                else
                {
                    if (vote.Value == (VoteValue)voteType)
                    {
                        vote.Value = VoteValue.Neutral;
                    }
                    else if (vote.Value != (VoteValue)voteType)
                    {
                        vote.Value = (VoteValue)voteType;
                    }
                }

                this.votes.Save();
                return this.votes.All()
                                    .Where(x => x.AnswerId == id)
                                    .Sum(x => (int)x.Value);
            }
            else if (subject == "comment")
            {
                vote = this.votes.All().FirstOrDefault(x => x.UserId == userId && x.CommentId == id);
                if (vote == null)
                {
                    vote = new Vote
                    {
                        UserId = userId,
                        CommentId = id,
                        Value = (VoteValue)voteType
                    };
                    this.votes.Add(vote);
                }
                else
                {
                    if (vote.Value == (VoteValue)voteType)
                    {
                        vote.Value = VoteValue.Neutral;
                    }
                    else if (vote.Value != (VoteValue)voteType)
                    {
                        vote.Value = (VoteValue)voteType;
                    }
                }

                this.votes.Save();

                return this.votes.All()
                                    .Where(x => x.CommentId == id)
                                    .Sum(x => (int)x.Value);
            }
            else
            {
                return 0;
            }
        }
    }
}
