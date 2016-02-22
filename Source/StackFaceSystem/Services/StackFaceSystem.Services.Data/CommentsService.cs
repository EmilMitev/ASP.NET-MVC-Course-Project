namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Contracts.Data;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;
    using StackFaceSystem.Services.Common;

    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> comments;
        private readonly IIdentifierProvider identifierProvider;

        public CommentsService(IDbRepository<Comment> comments, IIdentifierProvider identifierProvider)
        {
            this.comments = comments;
            this.identifierProvider = identifierProvider;
        }

        public void CreateComment(string commentId, Comment comment)
        {
            // var intPostId = this.identifierProvider.DecodeId(commentId);
            // comment.PostId = intPostId;
            // this.answers.Add(answer);
            // this.answers.Save();
        }
    }
}
