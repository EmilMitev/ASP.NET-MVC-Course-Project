namespace StackFaceSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IPostsService posts;

        public HomeController(IPostsService posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            var postsList = this.posts.GetPostsByPage(1, 5).To<PostsViewModel>().ToList();

            return this.View(postsList);
        }
    }
}