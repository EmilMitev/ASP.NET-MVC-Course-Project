namespace StackFaceSystem.Services.Data.Contracts
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IPostsService
    {
        Post GetById(string id);

        IQueryable<Post> GetPostsByPageAndSort(string sortType, string sortDirection, int page, int take);

        int GetPostsNumber();

        int GetPostsNumberByCategory(string name);

        IQueryable<Post> GetNewestPost();

        void CreatePost(Post post);

        IQueryable<Post> GetPostByCategory(string name, int page, int take);
    }
}