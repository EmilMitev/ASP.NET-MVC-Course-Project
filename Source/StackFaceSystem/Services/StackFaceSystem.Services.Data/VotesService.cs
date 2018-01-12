namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IDbRepository<Vote> m_Votes;

        public VotesService(IDbRepository<Vote> votes)
        {
            m_Votes = votes;
        }

        public int RegisterVote(string userId, string subject, int id, int voteType)
        {
            Vote existingVote;
            switch (subject)
            {
                case "post":
                    existingVote = m_Votes.All().FirstOrDefault(x => x.UserId == userId && x.PostId == id);
                    if (existingVote == null)
                    {
                        m_Votes.Add(new Vote
                        {
                            UserId = userId,
                            PostId = id,
                            Value = (VoteValue)voteType
                        });
                    }
                    else
                    {
                        GetVoteValue(voteType, existingVote);
                    }

                    m_Votes.Save();

                    return m_Votes.All().Where(x => x.PostId == id).Sum(x => (int)x.Value);
                case "answer":
                    existingVote = m_Votes.All().FirstOrDefault(x => x.UserId == userId && x.AnswerId == id);
                    if (existingVote == null)
                    {
                        m_Votes.Add(new Vote
                        {
                            UserId = userId,
                            AnswerId = id,
                            Value = (VoteValue)voteType
                        });
                    }
                    else
                    {
                        GetVoteValue(voteType, existingVote);
                    }

                    m_Votes.Save();
                    return m_Votes.All().Where(x => x.AnswerId == id).Sum(x => (int)x.Value);
                case "comment":
                    existingVote = m_Votes.All().FirstOrDefault(x => x.UserId == userId && x.CommentId == id);
                    if (existingVote == null)
                    {
                        m_Votes.Add(new Vote
                        {
                            UserId = userId,
                            CommentId = id,
                            Value = (VoteValue)voteType
                        });
                    }
                    else
                    {
                        GetVoteValue(voteType, existingVote);
                    }

                    m_Votes.Save();

                    return m_Votes.All().Where(x => x.CommentId == id).Sum(x => (int)x.Value);
                default:
                    return 0;
            }
        }

        private static void GetVoteValue(int voteType, Vote vote)
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
    }
}
