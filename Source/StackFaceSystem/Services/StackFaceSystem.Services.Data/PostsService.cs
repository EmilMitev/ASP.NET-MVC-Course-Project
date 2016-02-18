namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Common;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IDbRepository<Post> posts;
        private readonly IIdentifierProvider identifierProvider;

        public PostsService(IDbRepository<Post> posts, IIdentifierProvider identifierProvider)
        {
            this.posts = posts;
            this.identifierProvider = identifierProvider;
        }

        public Post GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var post = this.posts.GetById(intId);
            return post;
        }

        public IQueryable<Post> GetPostsByPage(int page, int count)
        {
            return this.posts
                .All()
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * count)
                .Take(count);
        }
    }
}
