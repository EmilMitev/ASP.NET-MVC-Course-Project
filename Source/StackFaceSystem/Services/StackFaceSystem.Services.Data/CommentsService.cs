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

        public void DeleteCommentByAnswerId(int answerId)
        {
            var comments = this.comments.All().Where(x => x.AnswerId == answerId).ToList();
            foreach (var comment in comments)
            {
                this.DeleteComment(comment);
            }
        }

        private void DeleteComment(Comment comment)
        {
            this.comments.Delete(comment);
            this.comments.Save();
        }
    }
}
