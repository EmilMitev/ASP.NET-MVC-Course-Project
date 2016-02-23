namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IAnswersService
    {
        void CreateAnswer(Answer answer);

        Answer GetById(int id);

        IQueryable<Answer> GetAnswerOnPost(int postId, int page, int take);

        int GetAnswerNumberPerPost(int postId);

        void UpdateAnswer(int answerId, string content);

        void DeleteAnswerByPostId(int postId);

        void DeleteAnswer(Answer answer);
    }
}