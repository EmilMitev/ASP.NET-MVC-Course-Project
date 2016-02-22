namespace StackFaceSystem.Services.Contracts.Data
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IPostsService
    {
        Post GetById(string id);

        IQueryable<Post> GetPostsByPage(int page, int count);

        int GetPostsNumber();

        int GetPostsNumberByCategory(string name);

        IQueryable<Post> GetNewestPost();

        void CreatePost(Post post);

        IQueryable<Post> GetPostByCategory(string name, int page, int take);
    }
}