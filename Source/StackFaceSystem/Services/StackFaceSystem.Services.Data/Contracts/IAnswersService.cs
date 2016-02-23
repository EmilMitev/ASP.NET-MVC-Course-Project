namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IAnswersService
    {
        IQueryable<Answer> GetAnswerOnPost(int postId, int page, int take);

        int GetAnswerNumberPerPost(int postId);

        void CreateAnswer(Answer answer);

        Answer GetById(int id);

        void DeleteAnswerByPostId(int postId);

        void DeleteAnswer(Answer answer);

        void UpdateAnswer(int answerId, string content);
    }
}