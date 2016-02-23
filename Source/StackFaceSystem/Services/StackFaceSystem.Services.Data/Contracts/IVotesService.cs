namespace StackFaceSystem.Services.Data.Contracts
{
    public interface IVotesService
    {
        int RegisterVote(string userId, string subject, int id, int voteType);
    }
}