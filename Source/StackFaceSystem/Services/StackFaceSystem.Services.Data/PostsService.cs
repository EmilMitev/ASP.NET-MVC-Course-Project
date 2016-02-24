namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Common;
    using Contracts;
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

        public void CreatePost(Post post)
        {
            this.posts.Add(post);
            this.posts.Save();
        }

        public Post GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var post = this.posts.GetById(intId);
            return post;
        }

        public int GetPostsCount()
        {
            return this.posts.All().Count();
        }

        public int GetPostsCountByCategory(string name)
        {
            return this.posts.All().Where(x => x.Category.Name == name).Count();
        }

        public IQueryable<Post> GetPostsByPageAndSort(string sortType, string sortDirection, int page, int take)
        {
            IQueryable<Post> posts = null;
            switch (sortType)
            {
                case "Date":
                    posts = sortDirection == "ascending" ? this.posts.All().OrderBy(x => x.CreatedOn) : this.posts.All().OrderByDescending(x => x.CreatedOn);
                    break;
                case "Title":
                    posts = sortDirection == "ascending" ? this.posts.All().OrderBy(x => x.Title) : this.posts.All().OrderByDescending(x => x.Title);
                    break;
                case "User":
                    posts = sortDirection == "ascending" ? this.posts.All().OrderBy(x => x.User.UserName) : this.posts.All().OrderByDescending(x => x.User.UserName);
                    break;
                case "Category":
                    posts = sortDirection == "ascending" ? this.posts.All().OrderBy(x => x.Category.Name) : this.posts.All().OrderByDescending(x => x.Category.Name);
                    break;
            }

            posts = posts.Skip((page - 1) * take)
                           .Take(take);

            return posts;
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

        public void UpdatePost(string postId, string title, string content)
        {
            var intId = this.identifierProvider.DecodeId(postId);
            var post = this.posts.GetById(intId);
            post.Title = title;
            post.Content = content;

            this.posts.Save();
        }

        public void DeletePost(Post post)
        {
            this.posts.Delete(post);
            this.posts.Save();
        }
    }
}
