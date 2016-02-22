namespace StackFaceSystem.Services.Contracts.Data
{
    using StackFaceSystem.Data.Models;

    public interface ICommentsService
    {
        void CreateComment(string commentId, Comment comment);
    }
}