namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IAnswersService
    {
        IQueryable<Answer> GetAnswerOnPost(int postId, int page, int take);

        int GetAnswerNumberPerPost(int postId);
    }
}