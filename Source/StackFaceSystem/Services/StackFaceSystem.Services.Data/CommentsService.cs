namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Services.Common;

    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> m_Comments;

        public CommentsService(IDbRepository<Comment> comments)
        {
            m_Comments = comments;
        }

        public void CreateComment(Comment comment)
        {
            m_Comments.Add(comment);
            m_Comments.Save();
        }

        public Comment GetById(int id)
        {
            return m_Comments.GetById(id);
        }

        public void UpdateComment(int commentId, string content)
        {
            var comment = m_Comments.GetById(commentId);
            comment.Content = content;

            m_Comments.Save();
        }

        public void DeleteCommentByAnswerId(int answerId)
        {
            var comments = m_Comments
                                .All()
                                .Where(x => x.AnswerId == answerId)
                                .ToList();

            foreach (var comment in comments)
            {
                DeleteComment(comment);
            }
        }

        public void DeleteComment(Comment comment)
        {
            m_Comments.Delete(comment);
            m_Comments.Save();
        }
    }
}
