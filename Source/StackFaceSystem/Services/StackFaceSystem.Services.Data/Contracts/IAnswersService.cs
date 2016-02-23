namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IAnswersService
    {
        IQueryable<Answer> GetAnswerOnPost(int postId, int page, int take);

        int GetAnswerNumberPerPost(int postId);

        void CreateAnswer(Answer answer);

        Answer GetAnswerById(int id);

        void DeleteAnswerByPostId(int postId);
    }
}