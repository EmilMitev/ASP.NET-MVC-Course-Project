namespace StackFaceSystem.Services.Data.Contracts
{
    using StackFaceSystem.Data.Models;

    public interface ICommentsService
    {
        void CreateComment(Comment comment);

        Comment GetById(int id);

        void UpdateComment(int commentId, string content);

        void DeleteCommentByAnswerId(int answerId);

        void DeleteComment(Comment comment);
    }
}