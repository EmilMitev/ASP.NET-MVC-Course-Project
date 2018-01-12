namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using Common;
    using Contracts;
    using StackFaceSystem.Data.Common;
    using StackFaceSystem.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IDbRepository<Post> m_Posts;
        private readonly IIdentifierProvider m_IdentifierProvider;

        public PostsService(IDbRepository<Post> posts, IIdentifierProvider identifierProvider)
        {
            m_Posts = posts;
            m_IdentifierProvider = identifierProvider;
        }

        public void CreatePost(Post post)
        {
            m_Posts.Add(post);
            m_Posts.Save();
        }

        public Post GetById(string id)
        {
            var intId = m_IdentifierProvider.DecodeId(id);
            return m_Posts.GetById(intId);
        }

        public int GetPostsCount()
        {
            return m_Posts
                        .All()
                        .Count();
        }

        public int GetPostsCountByCategory(string name)
        {
            return m_Posts
                        .All()
                        .Count(x => x.Category.Name == name);
        }

        public IQueryable<Post> GetPostsByPageAndSort(string sortType, string sortDirection, string search, int page, int take)
        {
            IQueryable<Post> posts = null;
            switch (sortType)
            {
                case "Date":
                    posts = sortDirection == "ascending" ? m_Posts.All().OrderBy(x => x.CreatedOn) : m_Posts.All().OrderByDescending(x => x.CreatedOn);
                    break;
                case "Title":
                    posts = sortDirection == "ascending" ? m_Posts.All().OrderBy(x => x.Title) : m_Posts.All().OrderByDescending(x => x.Title);
                    break;
                case "User":
                    posts = sortDirection == "ascending" ? m_Posts.All().OrderBy(x => x.User.UserName) : m_Posts.All().OrderByDescending(x => x.User.UserName);
                    break;
                case "Category":
                    posts = sortDirection == "ascending" ? m_Posts.All().OrderBy(x => x.Category.Name) : m_Posts.All().OrderByDescending(x => x.Category.Name);
                    break;
            }

            if (string.IsNullOrEmpty(search))
            {
                return posts?
                        .Skip((page - 1) * take)
                        .Take(take);
            }

            return posts?
                    .Where(x => x.Title.Contains(search))
                    .Skip((page - 1) * take)
                    .Take(take);
        }

        public IQueryable<Post> GetNewestPost()
        {
            return m_Posts
                        .All()
                        .OrderByDescending(x => x.CreatedOn)
                        .Take(5);
        }

        public IQueryable<Post> GetPostByCategory(string name, int page, int take)
        {
            return m_Posts
                        .All()
                        .Where(x => x.Category.Name == name)
                        .OrderByDescending(x => x.CreatedOn)
                        .Skip((page - 1) * take)
                        .Take(take);
        }

        public void UpdatePost(string postId, string title, string content)
        {
            var intId = m_IdentifierProvider.DecodeId(postId);
            var post = m_Posts.GetById(intId);
            post.Title = title;
            post.Content = content;

            m_Posts.Save();
        }

        public void DeletePost(Post post)
        {
            m_Posts.Delete(post);
            m_Posts.Save();
        }
    }
}
