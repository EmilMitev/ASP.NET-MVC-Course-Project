namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IPostsService
    {
        void CreatePost(Post post);

        Post GetById(string id);

        IQueryable<Post> GetPostsByPageAndSort(string sortType, string sortDirection, string search, int page, int take);

        int GetPostsCount();

        int GetPostsCountByCategory(string name);

        IQueryable<Post> GetNewestPost();

        IQueryable<Post> GetPostByCategory(string name, int page, int take);

        void UpdatePost(string postId, string title, string content);

        void DeletePost(Post post);
    }
}