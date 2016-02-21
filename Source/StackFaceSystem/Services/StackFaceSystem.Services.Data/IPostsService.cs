namespace StackFaceSystem.Services.Data
{
    using System.Linq;
    using StackFaceSystem.Data.Models;

    public interface IPostsService
    {
        Post GetById(string id);

        IQueryable<Post> GetPostsByPage(int page, int count);

        int GetPostsNumber();

        IQueryable<Post> GetNewestPost();

        void CreatePost(Post post);
    }
}