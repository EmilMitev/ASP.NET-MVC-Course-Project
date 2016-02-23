namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Services.Common;

    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> comments;

        public CommentsService(IDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public void CreateComment(Comment comment)
        {
            this.comments.Add(comment);
            this.comments.Save();
        }

        public Comment GetById(int id)
        {
            var comment = this.comments.GetById(id);
            return comment;
        }

        public void UpdateComment(int commentId, string content)
        {
            var comment = this.comments.GetById(commentId);
            comment.Content = content;

            this.comments.Save();
        }

        public void DeleteCommentByAnswerId(int answerId)
        {
            var comments = this.comments.All().Where(x => x.AnswerId == answerId).ToList();
            foreach (var comment in comments)
            {
                this.DeleteComment(comment);
            }
        }

        public void DeleteComment(Comment comment)
        {
            this.comments.Delete(comment);
            this.comments.Save();
        }
    }
}
