namespace StackFaceSystem.Services.Data.Contracts
{
    using StackFaceSystem.Data.Models;

    public interface ICommentsService
    {
        void CreateComment(Comment comment);

        void DeleteCommentByAnswerId(int answerId);

        Comment GetById(int id);

        void DeleteComment(Comment comment);

        void UpdateComment(int commentId, string content);
    }
}