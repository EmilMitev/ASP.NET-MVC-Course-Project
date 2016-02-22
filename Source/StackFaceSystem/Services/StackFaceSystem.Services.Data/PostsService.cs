namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Common;
    using Contracts.Data;
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

        public IQueryable<Post> GetPostsByPage(int page, int take)
        {
            var posts = this.posts
                            .All()
                            .OrderByDescending(x => x.CreatedOn)
                            .Skip((page - 1) * take)
                            .Take(take);
            return posts;
        }

        public int GetPostsNumber()
        {
            return this.posts.All().Count();
        }

        public int GetPostsNumberByCategory(string name)
        {
            return this.posts.All().Where(x => x.Category.Name == name).Count();
        }

        public IQueryable<Post> GetNewestPost()
        {
            return this.posts
                            .All()
                            .OrderByDescending(x => x.CreatedOn)
                            .Take(5);
        }

        public IQueryable<Post> GetPostByCategory(string name, int page, int take)
        {
            return this.posts
                            .All()
                            .Where(x => x.Category.Name == name)
                            .OrderByDescending(x => x.CreatedOn)
                            .Skip((page - 1) * take)
                            .Take(take);
        }

        public void CreatePost(Post post)
        {
            this.posts.Add(post);
            this.posts.Save();
        }
    }
}
